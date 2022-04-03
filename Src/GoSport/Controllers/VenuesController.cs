using GoSport.Core.Constants;
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

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            this.ViewData["Towns"] = this.townsService.GetAllTowns();
            var venue = this.venuesService.VenueById(id);
            if (venue == null)
            {
                this.TempData["Message"] = ConstCore.VenueDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { area = "" });
            }

            return this.View(venue);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(VenueViewModel model)
        {
            this.ViewData["Towns"] = this.townsService.GetAllTowns();

            var venue = this.venuesService.UpdateVenue(model);
            if (venue == null)
            {
                this.ViewData["Error"] = ConstCore.VenueWasNotUpdated;
                return this.View(model);
            }

            this.ViewData["Message"] = ConstCore.VenueWasUpdated;
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var venue = this.venuesService.VenueById(id);
            if (venue == null)
            {
                this.TempData["Message"] = ConstCore.VenueDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { area = "" });
            }

            return this.View(venue);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var venue = this.venuesService.VenueById(id);
            if (venue == null)
            {
                this.TempData["Message"] = ConstCore.VenueDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { area = "" });
            }

            return this.View(venue);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(VenueViewModel model)
        {
            this.venuesService.DeleteVenue(model);
            return this.RedirectToAction("All", "Venues", new { area = "" });
        }

        public IActionResult AllVenues()
        {
            this.ViewData["Towns"] = this.townsService.GetAllTownNames();
            return this.View();
        }

        [HttpPost]
        public IActionResult AllVenues(SearchTownViewModel model)
        {
            this.ViewData["Towns"] = this.townsService.GetAllTownNames();
            this.ViewData["Venues"] = this.venuesService.AllVenuesByTownId(model.TownId);
            return this.View();
        }
    }
}
