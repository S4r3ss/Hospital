using Hospital.Enums;
using Hospital.Filters;
using Hospital.Interfaces;
using Hospital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Hospital.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IAuditLogRepository _auditLog;
        private readonly IDoctorRepository _doctorRepository;

        public UserController(IUserRepository repository, IAuditLogRepository auditLog, IDoctorRepository doctorRepository)
        {
            _repository = repository;
            _auditLog = auditLog;
            _doctorRepository = doctorRepository;
        }


        [Route("user/updateuser/{userId}")]
        public IActionResult UpdateUser(User user, Guid userId, int rank)
        {
            var dbUser = _repository.GetUserById(userId);

            var newRole = _repository.getRoleIdByRank(rank);
            
            if (dbUser == null)
                return BadRequest();

            var changes = new List<AuditLog>();

            if(newRole != dbUser.UserRole)
            {
                changes.Add(new AuditLog
                {
                    Changes = user.Name + " " + user.Surname + " Role changed from [" + dbUser.UserRole+ "] to [" + newRole + "] (" + DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss") + ")",
                    AuditLogType = AuditLogType.Profile,
                    Id = Guid.NewGuid(),
                    WhoChange = userId
                });

                if (rank == 1)
                    _doctorRepository.CreateDoctor(dbUser);
                else
                    _doctorRepository.RemoveDoctor(userId);
                dbUser.UserRole = newRole;
            }

            if (dbUser.Address != user.Address)
            {
                changes.Add(new AuditLog
                {
                    Changes = user.Name + " " + user.Surname + " Address changed from [" + dbUser.Address + "] to [" + user.Address + "] (" + DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss") + ")",
                    AuditLogType = AuditLogType.Profile,
                    Id = Guid.NewGuid(),
                    WhoChange = userId
                });
                dbUser.Address = user.Address;
            }
            if (dbUser.Name != user.Name)
            {
                changes.Add(new AuditLog
                {
                    Changes = user.Name + " " + user.Surname + " Name changed from [" + dbUser.Name + "] to [" + user.Name + "] (" + DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss") + ")",
                    AuditLogType = AuditLogType.Profile,
                    Id = Guid.NewGuid(),
                    WhoChange = userId
                });
                dbUser.Name = user.Name;
            }

            if (dbUser.Surname != user.Surname)
            {
                changes.Add(new AuditLog
                {
                    Changes = user.Name + " " + user.Surname + " Surname changed from [" + dbUser.Surname + "] to [" + user.Surname + "] (" + DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss") + ")",
                    AuditLogType = AuditLogType.Profile,
                    Id = Guid.NewGuid(),
                    WhoChange = userId
                });
                dbUser.Surname = user.Surname;
            }

            if (dbUser.Birthday != user.Birthday)
            {
                changes.Add(new AuditLog
                {
                    Changes = user.Name + " " + user.Surname + " Birthday changed from [" + dbUser.Birthday + "] to [" + user.Birthday + "] (" + DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss") + ")",
                    AuditLogType = AuditLogType.Profile,
                    Id = Guid.NewGuid(),
                    WhoChange = userId
                });
                dbUser.Birthday = user.Birthday;
            }
            if (dbUser.Email != user.Email)
            {
                changes.Add(new AuditLog
                {
                    Changes = user.Name + " " + user.Surname + " Email changed from [" + dbUser.Email + "] to [" + user.Email + "] (" + DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss") + ")",
                    AuditLogType = AuditLogType.Profile,
                    Id = Guid.NewGuid(),
                    WhoChange = userId
                });
                dbUser.Email = user.Email;
            }
            if (dbUser.Gender != user.Gender)
            {
                changes.Add(new AuditLog
                {
                    Changes = user.Name + " " + user.Surname + " Gender changed from [" + dbUser.Gender + "] to [" + user.Gender + "] (" + DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss") + ")",
                    AuditLogType = AuditLogType.Profile,
                    Id = Guid.NewGuid(),
                    WhoChange = userId
                });
                dbUser.Gender = user.Gender;
            }
            if (dbUser.Phone != user.Phone)
            {
                changes.Add(new AuditLog
                {
                    Changes = user.Name + " " + user.Surname + " Phone changed from [" + dbUser.Phone + "] to [" + user.Phone + "] (" + DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss") + ")",
                    AuditLogType = AuditLogType.Profile,
                    Id = Guid.NewGuid(),
                    WhoChange = userId
                });
                dbUser.Phone = user.Phone;
            }

            if (changes.Count != 0)
            {
                foreach(var k in changes)
                    _auditLog.AddAuditLog(k);
                _repository.UpdateUser(dbUser);
            }
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }

        public IActionResult AllUsers(UsersFilter filter)
        {
            var users = _repository.getAllUsers(filter);
            return View(users);
        }

        public IActionResult Report(Guid doctorID)
        {
            var reportData = _repository.generateAppointmentReport(doctorID);
            return View(reportData);
        }

        public IActionResult AllUsersReport()
        {
            var resultData = _repository.GenerateAllUsersAppointmentReport();
            return View(resultData);
        }
    }
}
