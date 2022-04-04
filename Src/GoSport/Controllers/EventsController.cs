using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Event;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GoSport.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly UserManager<User> userManager;
        private readonly IOrganizersService organizersService;
        private readonly IDisciplinesService disciplinesService;
        private readonly IVenuesService venuesService;
        private readonly ITown townsService;
        public EventsController(IEventsService eventsService, UserManager<User> userManager, IOrganizersService organizersService, IDisciplinesService disciplinesService, IVenuesService venuesService, ITown townsService)
        {
            this.eventsService = eventsService;
            this.userManager = userManager;
            this.organizersService = organizersService;
            this.disciplinesService = disciplinesService;
            this.venuesService = venuesService;
            this.townsService = townsService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All(int? page)
        {
            var events = this.eventsService.AllEvents();

            var pageNumber = page ?? 1;
            var eventsPage = events.ToPagedList(pageNumber, 10);

            return this.View(eventsPage);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            this.ViewData["Organizers"] = this.organizersService.AllOrganizers();
            this.ViewData["Disciplines"] = this.disciplinesService.GetAllDisciplines();
            this.ViewData["Venues"] = this.venuesService.GetAllVenues();
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(CreateEventViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.eventsService.Add(model);
            return this.RedirectToAction("All", "Events", new { area = "" });
        }

    }
}
