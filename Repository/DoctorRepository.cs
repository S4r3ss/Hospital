using Hospital.Context;
using Hospital.Interfaces;
using Hospital.Models;
using Hospital.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DoctorRatingViewModel> GetAllDoctors()
        {

            var model = new List<DoctorRatingViewModel>();
            foreach (var k in _context.Doctors.ToList())
            {
                double rate = _context.DoctorsRatings.Where(x => x.DoctorID == k.Id).Select(x => x.Rating).Sum();
                double rateCount = _context.DoctorsRatings.Where(x => x.DoctorID == k.Id).Count();

                if(rateCount > 0)
                    model.Add(new DoctorRatingViewModel
                    {
                        Doctor = k,
                        DoctorsRating = rate / rateCount,
                    });
                else
                    model.Add(new DoctorRatingViewModel
                    {
                        Doctor = k,
                        DoctorsRating = 0,
                    });
            }

            return model;
        }

        public IEnumerable<DoctorRatingViewModel> GetDoctorByName(string name)
        {
            var model = new List<DoctorRatingViewModel>();
            var doctorList = _context.Doctors.Where(i => i.Name.Contains(name)).ToList();
            foreach (var k in doctorList)
            {
                var sum = _context.DoctorsRatings.Where(x => x.DoctorID == k.Id).Select(x => x.Rating).Sum();
                var count = _context.DoctorsRatings.Where(x => x.DoctorID == k.Id).Count();
                if(count != 0)
                {
                    model.Add(new DoctorRatingViewModel
                    {
                        Doctor = k,
                        DoctorsRating = sum / count,
                    });
                }
                else
                {
                    model.Add(new DoctorRatingViewModel
                    {
                        Doctor = k,
                        DoctorsRating = 0,
                    });
                }
            };

            return model;
        }

        public DoctorRatingViewModel GetDoctorDetails(Guid id)
        {
            double rating = _context.DoctorsRatings.Where(i => i.DoctorID == id).Select(i => i.Rating).Sum();
            int rateCount = _context.DoctorsRatings.Where(i => i.DoctorID == id).Count();
            var x = new DoctorRatingViewModel();
            if (rateCount > 0)
            {
                x = new DoctorRatingViewModel
                {
                    Doctor = _context.Doctors.Where(i => i.Id == id).FirstOrDefault(),
                    DoctorsRating = rating / rateCount
                };
            }
            else
            {
                x = new DoctorRatingViewModel
                {
                    Doctor = _context.Doctors.Where(i => i.Id == id).FirstOrDefault(),
                    DoctorsRating = 0
                };
            }

            return x;
        }

        public IEnumerable<Appointments> GetDoctorAppointments(Guid doctorId)
        {
            return _context.Appointments.Where(x => x.DoctorID == doctorId);
        }

        public void ChangeRating(int id, Guid doctorId, string userId)
        {

            var AlreadyVoted = _context.DoctorsRatings.Where(i => i.UserID == Guid.Parse(userId) && i.DoctorID == doctorId).FirstOrDefault();
            if(AlreadyVoted != null)
            {
                AlreadyVoted.Rating = id;
            }
            else
            {
                _context.Add(new DoctorsRating { DoctorID = doctorId, Rating =id, UserID = Guid.Parse(userId) });
            }
            _context.SaveChanges();
        }

        public void GenerateListOfDoctors()
        {
            var doctors = new List<Doctors>();
            var rnd = new Random();
            for(int i = 0; i < 20; i++)
            {
                doctors.Add(new Doctors
                {
                    Id = Guid.NewGuid(),
                    Address = "test Address",
                    Age = rnd.Next(0, 99),
                    Email = "test Email",
                    FullName = "test doctor " + i,
                    Name = "test " + i,
                    Number = "123",
                    Picture = "1.png",
                    Position = "doctor"
                });
            }

            _context.Doctors.AddRange(doctors);
            _context.SaveChanges();
        }

        public void CreateDoctor(User user)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (user.Birthday.Year * 100 + user.Birthday.Month) * 100 + user.Birthday.Day;

            int age = (a - b) / 10000;

            _context.Doctors.Add(new Doctors {
                Id = user.Id,
                Address = user.Address,
                Age = age,
                Email = user.Email,
                Name = user.Name,
                FullName = user.Name + " " + user.Surname,
                Number = user.Phone,
                Picture = "1.png",
                Position = "Doctor"
            });

            _context.SaveChanges();
        }

        public void RemoveDoctor(Guid doctorId)
        {
             var doctor = _context.Doctors.Where(x => x.Id == doctorId).AsNoTracking().FirstOrDefault();
            if (doctor != null)
            {
                _context.Doctors.Remove(new Doctors { Id = doctorId });
                _context.SaveChanges();
            }
        }
    }
}
