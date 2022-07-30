using System;

namespace Hospital.Models
{
    public class Pills
    {
        public Guid Id { get; set; }

        public double Price { get; set; }

        public string Name { get; set; }

        public string FormFactor { get; set; } // Лікарський засіб

        public string Producer { get; set; } // Виробник

        public string DosageForm { get; set; } // Лікарська форма 
        
        public string Picture { get; set; }

    }
}
