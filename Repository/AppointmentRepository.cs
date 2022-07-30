using Hospital.Context;
using Hospital.Interfaces;
using Hospital.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public void createAppointment(int day, int month, int hour, int minute, Guid doctorID, Guid userID)
        {
            _context.Appointments.Add(new Appointments
            {
                Day = day,
                Month = month,
                Hour = hour,
                Minute = minute,
                DoctorID = doctorID,
                UserID = userID
            });
            _context.SaveChanges();
        }

        public IEnumerable<Appointments> getAppointmentsByUserID(Guid userID)
        {
            return _context.Appointments.Where(x => x.UserID == userID).ToList();
        }
    }
}
