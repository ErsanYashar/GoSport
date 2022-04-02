using GoSport.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GoSport.Controllers
{
    public class VenuesController : Controller
    {
        private readonly IVenuesService venuesService;

        public VenuesController(IVenuesService venuesService)
        {
            this.venuesService = venuesService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All(int? page)
        {
            var venues = this.venuesService.GetAllVenues();

            var pageNumber = page ?? 1;
            var venuesPage = venues.ToPagedList(pageNumber, 10);

            return this.View(venuesPage);
        }
    }
}
