using System.Collections.Generic;

namespace Hospital.Models
{
    public class AllUsersAppointmentReport
    {
        public Doctors Doctor { get; set; }

        public List<Appointments> Appointment { get; set; }

        public double DoctorRating { get; set; }
    }
}
