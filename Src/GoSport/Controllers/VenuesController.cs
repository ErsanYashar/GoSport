using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Venue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GoSport.Controllers
{
    public class VenuesController : Controller
    {
        private readonly IVenuesService venuesService;
        private readonly ITown townsService;

        public VenuesController(IVenuesService venuesService, ITown townsService)
        {
            this.venuesService = venuesService;
            this.townsService = townsService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All(int? page)
        {
            var venues = this.venuesService.GetAllVenues();

            var pageNumber = page ?? 1;
            var venuesPage = venues.ToPagedList(pageNumber, 10);

            return this.View(venuesPage);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            this.ViewData["Towns"] = this.townsService.GetAllTowns();
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(AddVenueViewModel model)
        {
            this.ViewData["Towns"] = this.townsService.GetAllTowns();
            this.venuesService.AddVenue(model);
            return this.RedirectToAction("All", "Venues", new { area = "" });
        }
    }
}
