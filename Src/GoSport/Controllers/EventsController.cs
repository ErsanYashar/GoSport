using GoSport.Core.Constants;
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

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            this.ViewData["Organizers"] = this.organizersService.AllOrganizers();
            this.ViewData["Disciplines"] = this.disciplinesService.GetAllDisciplines();
            this.ViewData["Venues"] = this.venuesService.GetAllVenues();
            var eventUpdate = this.eventsService.EventUpdateById(id);

            if (eventUpdate == null)
            {
                this.TempData["Message"] = ConstCore.EventDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { area = "" });
            }

            return this.View(eventUpdate);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(UpdateEventViewModel model)
        {
            this.ViewData["Organizers"] = this.organizersService.AllOrganizers();
            this.ViewData["Disciplines"] = this.disciplinesService.GetAllDisciplines();
            this.ViewData["Venues"] = this.venuesService.GetAllVenues();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var updateEvent = this.eventsService.UpdateEvent(model);
            if (updateEvent == null)
            {
                this.ViewData["Error"] = ConstCore.EventWasNotUpdated;
                return this.View(model);
            }

            this.ViewData["Message"] = ConstCore.EventWasUpdated;
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var detailEvent = this.eventsService.GetEventById(id);
            if (detailEvent == null)
            {
                this.TempData["Message"] = ConstCore.EventDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { area = "" });
            }
            return this.View(detailEvent);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var eventDelete = this.eventsService.GetEventById(id);
            if (eventDelete == null)
            {
                this.TempData["Message"] = ConstCore.EventDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { area = "" });
            }
            return this.View(eventDelete);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(EventViewModel model)
        {
            this.eventsService.DeleteEvent(model);
            return this.RedirectToAction("All", "Events");
        }

        [HttpPost]
        public IActionResult EventsInTown(SearchTownEventViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["Town"] = this.townsService.GetAllTownNames();
                return this.RedirectToAction("Index", "Home");
            }

            this.ViewData["Town"] = this.townsService.GetTownById(model.TownId).Name;
            var events = this.eventsService.AllEventsInTown(model);

            return this.View(events);
        }

        [Authorize]
        public IActionResult UpcomingEvent(int id)
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var userEvent = this.eventsService.GetEventById(id);
            var isUserParticipate = this.eventsService.IsUserParticipate(user.Id, userEvent.Id);
            var hasFreeSeats = this.eventsService.CheckForFreeSpace(id);

            if (!hasFreeSeats)
            {
                this.ViewData["Error"] = ConstCore.NoFreeSeats;
            }

            if (isUserParticipate)
            {
                this.ViewData["Participate"] = true;
            }
            else
            {
                this.ViewData["Participate"] = false;
            }

            return this.View(userEvent);
        }

        [Authorize]
        public IActionResult Join(int id)
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var userEvent = this.eventsService.GetEventById(id);

            if (user == null || userEvent == null)
            {
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }

            this.eventsService.JoinUserToEvent(user.Id, userEvent.Id);
            return this.RedirectToAction("UpcomingEvent", "Events", new { id = id });
        }

        [Authorize]
        public IActionResult Leave(int id)
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var userEvent = this.eventsService.GetEventById(id);

            this.eventsService.LeaveUserFromEvent(user.Id, userEvent.Id);
            return this.RedirectToAction("UpcomingEvent", "Events", new { id = id });
        }

        [Authorize]
        public IActionResult MyEvents()
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var events = this.eventsService.GetEventsWithMyParticipation(user.UserName);
            return this.View(events);

        }

    }
}
