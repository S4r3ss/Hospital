using Hospital.Models;
using System;
using System.Collections.Generic;

namespace Hospital.Interfaces
{
    public interface IAuditLogRepository
    {
        public IEnumerable<string> GetProfileChanges(Guid userId);

        public void AddAuditLog(AuditLog auditLog);
    }
}
