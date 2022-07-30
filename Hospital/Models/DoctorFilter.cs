using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class DoctorFilter
    {
        public int currentPage { get; set; }

        public string? doctorName { get; set; }
    }
}
