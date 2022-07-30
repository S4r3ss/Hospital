using Hospital.Enums;
using System;

namespace Hospital.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; }

        public Guid WhoChange { get; set; }

        public AuditLogType AuditLogType { get; set; }

        public string Changes { get; set; }
    }
}
