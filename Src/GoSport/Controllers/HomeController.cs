using GoSport.Core.Constants;
using GoSport.Core.Services.Interfaces;
using GoSport.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoSport.Controllers
{
    public class HomeController : Controller
    {

        private readonly ITown townService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ITown townService)
        {
            _logger = logger;
            this.townService = townService;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole("Admin"))
            {
                return this.RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            this.ViewData[ConstCore.Town] = this.townService.GetAllTownNames();
            return this.View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Invalid()
        {
            this.ViewData["Message"] = this.TempData["Message"].ToString();
            return this.View();
        }

        public IActionResult AboutUs()
        {
            return this.View();
        }
    }
}