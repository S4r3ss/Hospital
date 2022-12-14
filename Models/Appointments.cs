using System;

namespace Hospital.Models
{
    public class Appointments
    {
        public Guid DoctorID { get; set; }

        public Guid UserID { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }
    }
}
