using System;

namespace Hospital.Models
{
    public class UserRoles
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string UserRole { get; set; }

        public string Email { get; set; }

        public int RolePos { get; set; }
    }
}
