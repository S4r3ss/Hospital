using Hospital.Models;
using Hospital.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Interfaces
{
    public interface IDoctorRepository
    {
        public IEnumerable<DoctorRatingViewModel> GetAllDoctors();

        public IEnumerable<DoctorRatingViewModel> GetDoctorByName(string name);

        public void ChangeRating(int id, Guid doctorId, string userId);

        public DoctorRatingViewModel GetDoctorDetails(Guid id);

        public IEnumerable<Appointments> GetDoctorAppointments(Guid doctorId);

        public void GenerateListOfDoctors();

        public void CreateDoctor(User user);

        public void RemoveDoctor(Guid doctorId);
    }
}
