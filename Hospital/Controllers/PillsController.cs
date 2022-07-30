using Hospital.Interfaces;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Hospital.Controllers
{
    public class PillsController : Controller
    {
        private readonly ILogger<PillsController> _logger;
        private readonly IPillsRepository _repository;

        public PillsController(ILogger<PillsController> logger, IPillsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Route("Pills/All/{currentPage}")]
        public IActionResult All(PillsFilter filter)
        {
            ViewData["CurPage"] = "3";
            var pills = _repository.GetAllPills();

            if (pills.Count() % 6 != 0)
                ViewBag.LastPageNumber = ((pills.Count() / 6) + 1).ToString();
            else
                ViewBag.LastPageNumber = (pills.Count() / 6).ToString();

            ViewBag.CurPageNumber = filter.currentPage.ToString();
            ViewBag.PreviousPageNumber = (filter.currentPage - 1).ToString();
            ViewBag.NextPageNumber = (filter.currentPage + 1).ToString();

            pills = pills.Skip((filter.currentPage.Value - 1) * 6).Take(6).ToList();
            return View(pills);
        }

        [Route("Pills/All/{currentPage}/{name}")]

        public IActionResult ByName(PillsFilter filter)
        {
            ViewData["CurPage"] = "3";
            var pills = _repository.GetPillByName(filter.name);

            if (pills.Count() % 6 != 0)
                ViewBag.LastPageNumber = ((pills.Count() / 6) + 1).ToString();
            else
                ViewBag.LastPageNumber = (pills.Count() / 6).ToString();

            ViewBag.CurPageNumber = filter.currentPage.ToString();
            ViewBag.PreviousPageNumber = (filter.currentPage - 1).ToString();
            ViewBag.NextPageNumber = (filter.currentPage + 1).ToString();

            pills = pills.Skip((filter.currentPage.Value - 1) * 6).Take(6).ToList();
            return View("All", pills);
        }

        public IActionResult ByName(string PillsName)
        {
            return ByName(new PillsFilter { currentPage = 1, name = PillsName });
        }

        [Route("Pills/Details/{Id}")]
        public IActionResult Details(Guid Id)
        {
            ViewData["CurPage"] = "3";
            return View(_repository.Details(Id));
        }
    }
}
