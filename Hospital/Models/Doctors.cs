using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Doctors
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string Position { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string Picture { get; set; }

        public string Address { get; set; }
    }
}
