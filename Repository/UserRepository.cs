using Hospital.Context;
using Hospital.Filters;
using Hospital.Interfaces;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User GetUserById(Guid id)
        {
            return _context.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool isDoctor(Guid UserID)
        {
            var userData = _context.Users.Where(x => x.Id == UserID).FirstOrDefault();

            var userRole = _context.Roles.Where(x => x.RoleID == userData.UserRole).FirstOrDefault();

            return userRole.RoleName == "Head Doctor";
        }

        public IEnumerable<UserRoles> getAllUsers(UsersFilter filter)
        {
            var result = new List<UserRoles>();
            if (filter.rank == -1)
            {
                var entryPoint = _context.Users
                    .Join(
                        _context.Roles,
                        user => user.UserRole,
                        role => role.RoleID,
                        (user, role) => new
                        {
                            user.Id,
                            user.Name,
                            user.Surname,
                            user.Email,
                            user.Address,
                            role.RoleName,
                            user.Phone,
                            user.Gender,
                            user.Birthday,
                            user.UserRole,
                            role.RoleOrder
                        }
                    ).Where(x => EF.Functions.Like(x.Name, "%"+filter.userName+"%")).ToList();

                foreach (var k in entryPoint)
                {
                    result.Add(new UserRoles { Id = k.Id, Address = k.Address, Birthday = k.Birthday, Email = k.Email, Gender = k.Gender, Name = k.Name, Phone = k.Phone, Surname = k.Surname, UserRole = k.RoleName, RolePos = k.RoleOrder });
                }
            }
            else
            {
                Guid roleOrder = _context.Roles.Where(x => x.RoleOrder == filter.rank).Select(x => x.RoleID).FirstOrDefault();
                var entryPoint = _context.Users
                    .Join(
                        _context.Roles,
                        user => user.UserRole,
                        role => role.RoleID,
                        (user, role) => new
                        {
                            user.Id,
                            user.Name,
                            user.Surname,
                            user.Email,
                            user.Address,
                            role.RoleName,
                            user.Phone,
                            user.Gender,
                            user.Birthday,
                            user.UserRole,
                            role.RoleOrder
                        }
                    ).Where(x => EF.Functions.Like(x.Name, "%" + filter.userName + "%") && x.UserRole == roleOrder).ToList();

                foreach (var k in entryPoint)
                {
                    result.Add(new UserRoles { Id = k.Id, Address = k.Address, Birthday = k.Birthday, Email = k.Email, Gender = k.Gender, Name = k.Name, Phone = k.Phone, Surname = k.Surname, UserRole = k.RoleName, RolePos = k.RoleOrder });
                }
            }
            return result;
        }

        public AppointmentReport generateAppointmentReport(Guid doctorID)
        {
            var data = _context.Appointments.Where(x => x.DoctorID == doctorID).ToList();

            var result = new AppointmentReport();

            result.January = _context.Appointments.Where(x => x.DoctorID == doctorID && x.Month == 1).Count();
            result.February = _context.Appointments.Where(x => x.DoctorID == doctorID && x.Month == 2).Count();
            result.March = _context.Appointments.Where(x => x.DoctorID == doctorID && x.Month == 3).Count();
            result.April = _context.Appointments.Where(x => x.DoctorID == doctorID && x.Month == 4).Count();
            result.May = _context.Appointments.Where(x => x.DoctorID == doctorID && x.Month == 5).Count();
            result.June = _context.Appointments.Where(x => x.DoctorID == doctorID && x.Month == 6).Count();
            result.July = _context.Appointments.Where(x => x.DoctorID == doctorID && x.Month == 7).Count();
            result.August = _context.Appointments.Where(x => x.DoctorID == doctorID && x.Month == 8).Count();
            result.September = _context.Appointments.Where(x => x.DoctorID == doctorID && x.Month == 9).Count();
            result.October = _context.Appointments.Where(x => x.DoctorID == doctorID && x.Month == 10).Count();
            result.November = _context.Appointments.Where(x => x.DoctorID == doctorID && x.Month == 11).Count();
            result.December = _context.Appointments.Where(x => x.DoctorID == doctorID && x.Month == 12).Count();
            result.UserName = _context.Doctors.Where(x => x.Id == doctorID).Select(x => x.FullName).FirstOrDefault();
            return result;
        }

        public IEnumerable<AllUsersAppointmentReport> GenerateAllUsersAppointmentReport()
        {
            var result = new List<AllUsersAppointmentReport>();

            var allDoctors = _context.Doctors.ToList();

            for(int i = 0; i < allDoctors.Count(); i++)
            {
                var DoctorsRating = _context.DoctorsRatings.Where(x => x.DoctorID == allDoctors[i].Id).Select(x => x.Rating).Sum();
                var DoctorsCount = _context.DoctorsRatings.Where(x => x.DoctorID == allDoctors[i].Id).Select(x => x.Rating).Count();
                if (DoctorsCount == 0)
                {
                    result.Add(new AllUsersAppointmentReport
                    {
                        Appointment = _context.Appointments.Where(x => x.DoctorID == allDoctors[i].Id).ToList(),
                        Doctor = allDoctors[i],
                        DoctorRating = 0
                    });
                }
                else
                {
                    result.Add(new AllUsersAppointmentReport
                    {
                        Appointment = _context.Appointments.Where(x => x.DoctorID == allDoctors[i].Id).ToList(),
                        Doctor = allDoctors[i],
                        DoctorRating = DoctorsRating / DoctorsCount
                    });
                }
            }

            return result;
        }

        public Guid getRoleIdByRank(int rank)
        {
            return _context.Roles.Where(x => x.RoleOrder == rank).Select(x => x.RoleID).FirstOrDefault();
        }

        public int getRoleRankByID(Guid roleID)
        {
            return _context.Roles.Where(x => x.RoleID == roleID).Select(x => x.RoleOrder).FirstOrDefault();
        }
    }
}
