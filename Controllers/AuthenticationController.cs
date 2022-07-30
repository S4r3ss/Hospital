using Hospital.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hospital.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationRepository _repository;
        private readonly IAuditLogRepository _auditLog;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            ILogger<AuthenticationController> logger, 
            IAuthenticationRepository repository, 
            IConfiguration configuration, 
            IUserRepository userRepository,
            IAuditLogRepository auditLogRepository)
        {
            _logger = logger;
            _repository = repository;
            _configuration = configuration;
            _userRepository = userRepository;
            _auditLog = auditLogRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["CurPage"] = "Login";
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public IActionResult PostLogin(string email, string password)
        {
            var user = _repository.ValidateUserLogin(email, password);
            if (user != null)
            {
                ViewData["CurPage"] = "0";

                string Token = GenerateToken(email);
                var expireDate = DateTimeOffset.UtcNow.AddMinutes(20);

                HttpContext.Response.Cookies.Append("Token", Token, 
                    new CookieOptions
                    {
                        Expires = expireDate
                    });
                HttpContext.Response.Cookies.Append("UserName", user.Name, 
                    new CookieOptions
                    {
                        Expires = expireDate
                    });
                HttpContext.Session.SetString("UserID", user.Id.ToString());
                HttpContext.Session.Remove("Error");
                _repository.UpdateToken(Token, user.Id, expireDate, Request.Cookies["Token"]);

                return RedirectToRoute(new { controller = "Doctor", action = "Index" });
            }
            else
            {
                ViewData["CurPage"] = "Login";
                return View();
            }
        }

        public IActionResult Register(string error)
        {
            ViewData["CurPage"] = "Register";
            dynamic model = new ExpandoObject();
            model.Error = error;
            return View(model);
        }

        [HttpPost]
        [ActionName("Register")]
        public IActionResult PostRegister(string email, string password, string repeatPassword)
        {
            if(password != repeatPassword)
                return RedirectToAction("Register", new { error = "Паролі не збігаються" });

            if(_repository.CheckUniqEmail(email))
                return RedirectToAction("Register", new { error = "Користувач з такою поштою уже існує" });

            if (password.Length <= 5)
                return RedirectToAction("Register", new { error = "Мінімальний розмір пароля - 6 символів" });

            _repository.CreateUser(email, password);
            return RedirectToAction("Index", "Doctor");
        }

        public IActionResult UserPage(Guid userId)
        {
            if(_repository.ValidateUser(userId))
            {
                dynamic model = new ExpandoObject();
                model.User = _userRepository.GetUserById(userId);
                model.AuditLog = _auditLog.GetProfileChanges(userId);
                model.isDoctor = _userRepository.isDoctor(userId);
                return View(model);
            }

            HttpContext.Response.Cookies.Delete("UserName");
            HttpContext.Session.Remove("UserID");
            HttpContext.Response.Cookies.Delete("Token");
            return RedirectToAction("Login");
        }

        public IActionResult TargetUserPage(Guid userId)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                HttpContext.Response.Cookies.Delete("UserName");
                HttpContext.Session.Remove("UserID");
                HttpContext.Response.Cookies.Delete("Token");

                return RedirectToAction("Index", "Doctor");
            }

            dynamic model = new ExpandoObject();
            model.User = _userRepository.GetUserById(userId);
            model.AuditLog = _auditLog.GetProfileChanges(userId);
            model.isDoctor = _userRepository.isDoctor(userId);
            model.RoleRank = _userRepository.getRoleRankByID(model.User.UserRole);
            return View(model);
        }

        public IActionResult Logout()
        {
            _repository.RemoveToken(Guid.Parse(HttpContext.Session.GetString("UserID")));
            HttpContext.Response.Cookies.Delete("UserName");
            HttpContext.Session.Remove("UserID");
            HttpContext.Response.Cookies.Delete("Token");

            return RedirectToAction("Index", "Doctor");
        }

        private string GenerateToken(string email)
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email),
                }),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "Issuer",
                Audience = "Audience"
            };
            var token = tokenHandle.CreateToken(tokenDescriptor);
            return tokenHandle.WriteToken(token);
        }
    }
}
