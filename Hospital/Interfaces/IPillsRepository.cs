using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Interfaces
{
    public interface IPillsRepository
    {
        public IEnumerable<Pills> GetAllPills();

        public IEnumerable<Pills> GetPillByName(string name);

        public Pills Details(Guid Id);
    }
}
