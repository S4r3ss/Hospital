using Hospital.Context;
using Hospital.Enums;
using Hospital.Interfaces;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Repository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<string> GetProfileChanges(Guid userId)
        {
            var auditLogEntries = _context.AuditLogs.Where(x => x.WhoChange == userId && x.AuditLogType == AuditLogType.Profile);
            var changes = new List<string>();
            foreach(var al in auditLogEntries)
            {
                changes.Add(al.Changes);
            }
            return changes;
        }

        public void AddAuditLog(AuditLog auditLog)
        {
            _context.AuditLogs.Add(auditLog);
            _context.SaveChanges();
        }
    }
}
