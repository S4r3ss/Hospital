using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Interfaces
{
    public interface IHomeRepository
    {
        public IEnumerable<Doctors> GetAllDoctors();

        public IEnumerable<Doctors> GetDoctorByName(string name);

        public Doctors GetDoctorDetails(Guid id);
    }
}
