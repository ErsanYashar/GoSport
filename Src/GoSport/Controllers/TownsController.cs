using GoSport.Core.Constants;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Town;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GoSport.Controllers
{
    public class TownsController : Controller
    {
        private readonly ITown townsService;

        public TownsController(ITown townsService)
        {
            this.townsService = townsService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All(int? page)
        {
            var towns = this.townsService.GetAllTowns();
            var pageNumber = page ?? 1;
            var townsPage = towns.ToPagedList(pageNumber, 10);

            return this.View(townsPage);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(TownViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.townsService.AddTown(model);

            return this.RedirectToAction("All", "Towns", new { area = "" });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var town = this.townsService.GetTownById(id);

            if (town == null)
            {
                this.TempData["Message"] = ConstCore.TownDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { area = "" });
            }

            return this.View(town);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(TownViewModel model)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var town = this.townsService.UpdateTown(model);

            if (town == null)
            {
                this.ViewData["Error"] = ConstCore.TownWasNotUpdated;
                return this.View(model);
            }

            this.ViewData["Message"] = ConstCore.TownWasUpdated;

            return this.View();
        }
    }
}
