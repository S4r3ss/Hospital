using Hospital.Models;
using System;
using System.Collections.Generic;

namespace Hospital.Interfaces
{
    public interface IAppointmentRepository
    {
        public void createAppointment(int day, int month, int hour, int minute, Guid doctorID, Guid userID);

        public IEnumerable<Appointments> getAppointmentsByUserID(Guid userID);
    }
}
