using System;

namespace Hospital.Models
{
    public class UserToken
    {
        public Guid UserID { get; set; }

        public string Token { get; set; }

        public DateTimeOffset ExpireDate { get; set; }
    }
}
