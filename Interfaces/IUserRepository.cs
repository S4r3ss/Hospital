using Hospital.Filters;
using Hospital.Models;
using System;
using System.Collections.Generic;

namespace Hospital.Interfaces
{
    public interface IUserRepository
    {
        public User GetUserById(Guid id);

        public void UpdateUser(User user);

        public bool isDoctor(Guid UserID);

        public IEnumerable<UserRoles> getAllUsers(UsersFilter filter);

        public AppointmentReport generateAppointmentReport(Guid doctorID);

        public IEnumerable<AllUsersAppointmentReport> GenerateAllUsersAppointmentReport();

        public Guid getRoleIdByRank(int rank);

        public int getRoleRankByID(Guid roleID);
    }
}
