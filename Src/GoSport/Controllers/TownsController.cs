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

    }
}
