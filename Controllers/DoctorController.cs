using Hospital.Context;
using Hospital.Filters;
using Hospital.Interfaces;
using Hospital.Models;
using Hospital.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ILogger<DoctorController> _logger;
        private readonly IDoctorRepository _repository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IHttpContextAccessor _ca;
        private readonly IAuthenticationRepository _authentication;


        public DoctorController(
            ILogger<DoctorController> logger,
            IDoctorRepository repository, 
            IHttpContextAccessor ca,
            IAuthenticationRepository authentication,
            IAppointmentRepository appointmentRepository)
        {
            _logger = logger;
            _repository = repository;
            _ca = ca;
            _authentication = authentication;
            _appointmentRepository = appointmentRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewData["CurPage"] = "0";
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            ViewData["CurPage"] = "2";
            return View();
        }

        public IActionResult Doctors(string doctorName)
        {
            return Doctors(new DoctorFilter { currentPage = 1, doctorName = doctorName });
        }

        public IActionResult AllDoctors()
        {
            ViewData["DoctorFilter"] = null;
            return RedirectToAction("Doctors");
        }

        public IActionResult Reform()
        {
            ViewData["CurPage"] = "2";
            return View();
        }

        [Route("doctors/{currentPage}/{doctorName}")]
        [Route("doctors/{currentPage}")]
        public IActionResult Doctors(DoctorFilter filter)
        {
            ViewData["CurPage"] = "1";

            var d = Request.Cookies["Token"];
            IEnumerable<DoctorRatingViewModel> doctors = null;
            if (filter.doctorName != null)
            {
                filter.currentPage = 1;
                ViewData["DoctorFilter"] = filter.doctorName;
                doctors = _repository.GetDoctorByName(filter.doctorName);
            }
            else
            {
                doctors = _repository.GetAllDoctors();
            }

            if(doctors.Count() % 6 != 0)
                ViewBag.LastPageNumber = ((doctors.Count() / 6)+1).ToString();
            else
                ViewBag.LastPageNumber = (doctors.Count() / 6).ToString();

            var x = new List<DoctorRatingViewModel>();

            if (filter.currentPage != 1)
            {
                foreach(var k in doctors)
                {
                    x.Add(new DoctorRatingViewModel
                    {
                        Doctor = k.Doctor,
                        DoctorsRating = k.DoctorsRating
                    });
                }
                x = x.Skip((filter.currentPage - 1) * 6).Take(6).ToList();
                ViewBag.CurPageNumber = filter.currentPage.ToString();
                ViewBag.NextPageNumber = (filter.currentPage + 1).ToString();
                ViewBag.PreviousPageNumber = (filter.currentPage - 1).ToString();
            }
            else
            {
                foreach (var k in doctors)
                {
                    x.Add(new DoctorRatingViewModel
                    {
                        Doctor = k.Doctor,
                        DoctorsRating = k.DoctorsRating
                    });
                }
                x = x.Take(6).ToList();
                ViewBag.CurPageNumber = "1";
                ViewBag.NextPageNumber = 2;
            }

            return View(x);
        }

        [Route("doctor/doctordetails/{id}")]
        public IActionResult DoctorDetails(Guid id)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                HttpContext.Response.Cookies.Delete("UserName");
                HttpContext.Session.Remove("UserID");
                HttpContext.Response.Cookies.Delete("Token");

                return RedirectToAction("Index", "Doctor");
            }
            ViewData["CurPage"] = "1";
            dynamic model = new ExpandoObject();
            model.Doctor = _repository.GetDoctorDetails(id);
            model.Appointments = _repository.GetDoctorAppointments(id);
            model.Today = DateTime.Now.Day;
            model.MaxDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            model.StringMonth = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("us"));
            model.NextMonth = DateTime.Now.AddMonths(1).ToString("MMMM", CultureInfo.CreateSpecificCulture("us"));
            model.UserDates = _appointmentRepository.getAppointmentsByUserID(Guid.Parse(HttpContext.Session.GetString("UserID")));
            return View(model);
        }

        [Route("Doctor/ChangeRating/{id}/{doctorId}")]
        public IActionResult ChangeRating(int id, Guid doctorId)
        {
            ViewData["CurPage"] = "1";
            if (HttpContext.Session.GetString("UserID") != null && _authentication.ValidateUser(Guid.Parse(HttpContext.Session.GetString("UserID"))))
            {
                _repository.ChangeRating(id, doctorId, HttpContext.Session.GetString("UserID"));
                return RedirectToAction("Doctors");
            }
            else
            {
                return Unauthorized();
            }
        }

        public IActionResult S4ressGenerateDoctors()
        {
            _repository.GenerateListOfDoctors();
            return RedirectToAction("Index", "doctor");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (HttpContext.Response.StatusCode == 401)
            {
                HttpContext.Session.SetInt32("Error", 401);
                return RedirectToAction("Index");
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, StatusCode = HttpContext.Response.StatusCode });
            }
        }
    }
}
