using Hospital.Context;
using Hospital.Interfaces;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Repository
{
    public class PillRepository : IPillsRepository
    {
        private readonly AppDbContext _context;

        public PillRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Pills> GetAllPills()
        {
            return _context.Pills.ToList();
        }

        public IEnumerable<Pills> GetPillByName(string name)
        {
            return _context.Pills.Where(x => x.Name.Contains(name)).ToList();
        }

        public Pills Details(Guid Id)
        {
            return _context.Pills.Where(x => x.Id == Id).FirstOrDefault();
        }
    }
}
