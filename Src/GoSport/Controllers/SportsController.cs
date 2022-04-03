using GoSport.Core.Constants;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Sport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GoSport.Controllers
{
    public class SportsController : Controller
    {
        private readonly ISportsService sportsService;
        private readonly IDisciplinesService disciplinesService;

        public SportsController(ISportsService sportsService, IDisciplinesService disciplinesService)
        {
            this.sportsService = sportsService;
            this.disciplinesService = disciplinesService;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult All(int? page)
        {
            var sports = this.sportsService.GetAllSports();

            var pageNumber = page ?? 1;
            var sportsPage = sports.ToPagedList(pageNumber, 10);

            return this.View(sportsPage);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(AddSportViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.sportsService.Add(model);

            return this.RedirectToAction("All", "Sports", new { area = "" });
        }

        public IActionResult AllSports()
        {
            var sports = this.sportsService.GetAllSports();
            return this.View(sports);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var sport = this.sportsService.GetSportById(id);

            if (sport == null)
            {
                this.TempData["Message"] = ConstCore.SportDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { area = "" });
            }

            return this.View(sport);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(SportViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var sport = this.sportsService.UpdateSport(model);

            if (sport == null)
            {
                this.ViewData["Error"] = ConstCore.SportWasNotUpdated;
                return this.View(model);
            }

            this.ViewData["Message"] = ConstCore.SportWasUpdated;
            return this.View();
        }

        public IActionResult Details(int id)
        {
            var sport = this.sportsService.GetSportById(id);

            if (sport == null)
            {
                this.TempData["Message"] = ConstCore.SportNotExist;
                return this.RedirectToAction("Invalid", "Home", new { area = "" });
            }

            this.ViewData["Disciplines"] = this.disciplinesService.GetDisciplinesBySportId(id);

            return this.View(sport);
        }


    }
}
