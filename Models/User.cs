using System;

namespace Hospital.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

        public Guid UserRole { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
