using Hospital.Models;
using System;
using System.Collections.Generic;

namespace Hospital.Wrappers
{
    public class DoctorRatingViewModel
    {
        public Doctors Doctor { get; set; }
        public double DoctorsRating { get; set; }
    }
}
