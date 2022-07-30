using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class DoctorsRating
    {
        public Guid DoctorID { get; set; }

        public Guid UserID { get; set; }

        public double Rating { get; set; }
    }
}
