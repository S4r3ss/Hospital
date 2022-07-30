using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    public class InsuranceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
