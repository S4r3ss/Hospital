using Hospital.Models;
using System;

namespace Hospital.Interfaces
{
    public interface IAuthenticationRepository
    {
        public User ValidateUserLogin(string login, string password);

        public void UpdateToken(string Token, Guid userId, DateTimeOffset expireDate, string oldToken = null);

        public bool ValidateUser(Guid userId);

        public void RemoveToken(Guid userId);

        public bool CheckUniqEmail(string email);

        public void CreateUser(string email, string password);

        public User GetUserById(Guid userId);
    }
}
