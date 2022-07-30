using Hospital.Context;
using Hospital.Interfaces;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly AppDbContext _context;

        public HomeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Doctors> GetAllDoctors()
        {
            return _context.Doctors.ToList();
        }

        public IEnumerable<Doctors> GetDoctorByName(string name)
        {
            return _context.Doctors.Where(i => i.Name.Contains(name));
        }

        public Doctors GetDoctorDetails(Guid id)
        {
            return _context.Doctors.Where(i => i.Id == id).FirstOrDefault();
        }
    }
}
