using Hospital.Context;
using Hospital.Enums;
using Hospital.Interfaces;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Hospital.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AppDbContext _context;

        public AuthenticationRepository(AppDbContext context)
        {
            _context = context;
        }

        public User ValidateUserLogin(string login, string password)
        {
            return _context.Users.Where(x => x.Email == login && x.Password == password).FirstOrDefault();
        }

        public void UpdateToken(string Token, Guid userId, DateTimeOffset expireDate, string oldToken = null)
        {
            if(userId == null || Token == null)
            {
                return;
            }

            var tokens = _context.UserTokens.Where(x => x.UserID == userId);
            if(tokens.Any())
            {
                _context.RemoveRange(tokens);
                _context.SaveChanges();
            }

            _context.Add(new UserToken { Token = Token, UserID = userId, ExpireDate = expireDate });
            _context.SaveChanges();
        }

        public void RemoveToken(Guid userId)
        {
            if (userId == null)
                return;
            
            var token = _context.UserTokens.Where(x => x.UserID == userId).FirstOrDefault();

            if(token != null)
            {
                _context.Remove(token);
                _context.SaveChanges();
            }
        }

        public bool ValidateUser(Guid userId)
        {
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            var userTokens = _context.UserTokens.Where(x => x.UserID == userId).AsNoTracking();

            if(user != null && userTokens != null && userTokens.FirstOrDefault()?.ExpireDate >= DateTime.UtcNow)
            {
                return true;
            }

            if (userTokens.Count() != 0)
            {
                _context.UserTokens.RemoveRange(userTokens);
                _context.SaveChanges();
            }
            return false;
        }

        public bool CheckUniqEmail(string email)
        {
            return _context.Users.Where(x => x.Email == email).Any();
        }

        public User GetUserById(Guid userId)
        {
            return _context.Users.Where(x => x.Id == userId).FirstOrDefault();
        }

        public void CreateUser(string email, string password)
        {
            if (!CheckUniqEmail(email))
            {
                var clientRole = _context.Roles.Where(x => x.RoleOrder == 0).Select(x => x.RoleID).FirstOrDefault();
                _context.Users.Add(new User { Id = Guid.NewGuid(), Email = email, Password = password, Address = "", Birthday = DateTime.UtcNow, Gender = Gender.Male.ToString(), Name = email, Phone = "", UserRole = clientRole, Surname = "" });
                _context.SaveChanges();
            }
        }
    }
}
