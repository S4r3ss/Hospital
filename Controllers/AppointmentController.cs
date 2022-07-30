using Hospital.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;

namespace Hospital.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAppointmentRepository _repository;
        public AppointmentController(
            ILogger<AppointmentController> logger,
            IAppointmentRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

        public IActionResult CreateAppointment(int day, string month, int hour, int minute, string doctorID)
        {
            int mon;

            if(HttpContext.Session.GetString("UserID") == null)
            {
                HttpContext.Response.Cookies.Delete("UserName");
                HttpContext.Session.Remove("UserID");
                HttpContext.Response.Cookies.Delete("Token");

                return RedirectToAction("Index", "Doctor");
            }

            if (month == null)
                mon = DateTime.Now.Month;
            else
                mon = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;

            _repository.createAppointment(day, mon, hour, minute, Guid.Parse(doctorID), Guid.Parse(HttpContext.Session.GetString("UserID")));
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }
    }
}
