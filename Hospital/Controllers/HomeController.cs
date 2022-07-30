using Hospital.Context;
using Hospital.Interfaces;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _repository;


        public HomeController(ILogger<HomeController> logger, IHomeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            ViewData["CurPage"] = "0";
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["CurPage"] = "2";
            return View();
        }

        public IActionResult Doctors(string doctorName)
        {
            return Doctors(new DoctorFilter { currentPage = 1, doctorName = doctorName });
        }

        public IActionResult AllDoctors()
        {
            ViewData["DoctorFilter"] = null;
            return RedirectToAction("Doctors");
        }

        public IActionResult Reform()
        {
            ViewData["CurPage"] = "2";
            return View();
        }

        [Route("doctors/{currentPage}/{doctorName}")]
        [Route("doctors/{currentPage}")]
        public IActionResult Doctors(DoctorFilter filter)
        {
            ViewData["CurPage"] = "1";
            
            IEnumerable<Doctors> doctors = null;
            if (filter.doctorName != null)
            {
                filter.currentPage = 1;
                ViewData["DoctorFilter"] = filter.doctorName;
                doctors = _repository.GetDoctorByName(filter.doctorName);
            }
            else
            {
                doctors = _repository.GetAllDoctors();
            }

            if(doctors.Count() % 6 != 0)
                ViewBag.LastPageNumber = ((doctors.Count() / 6)+1).ToString();
            else
                ViewBag.LastPageNumber = (doctors.Count() / 6).ToString();

            List<Doctors> x;

            if (filter.currentPage != 1)
            {
                x = doctors.Skip((filter.currentPage - 1) * 6).Take(6).ToList();
                ViewBag.CurPageNumber = filter.currentPage.ToString();
                ViewBag.NextPageNumber = (filter.currentPage + 1).ToString();
                ViewBag.PreviousPageNumber = (filter.currentPage - 1).ToString();
            }
            else
            {
                x = doctors.Take(6).ToList();
                ViewBag.CurPageNumber = "1";
                ViewBag.NextPageNumber = 2;
            }

            return View(x);
        }

        [Route("home/doctordetails/{id}")]
        public IActionResult DoctorDetails(Guid id)
        {
            ViewData["CurPage"] = "1";
            return View(_repository.GetDoctorDetails(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
