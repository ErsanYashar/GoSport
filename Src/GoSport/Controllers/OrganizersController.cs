using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Organizer;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GoSport.Controllers
{
    public class OrganizersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IOrganizersService organizersService;

        public OrganizersController(UserManager<User> userManager, IOrganizersService organizersService)
        {
            this.userManager = userManager;
            this.organizersService = organizersService;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult All(int? page)
        {
            var organizers = this.organizersService.AllOrganizers();

            var pageNumber = page ?? 1;
            var organizersPage = organizers.ToPagedList(pageNumber, 10);

            return this.View(organizersPage);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {

            return this.View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(AddOrganizerViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.organizersService.Add(model, this.User.Identity.Name);
            return this.RedirectToAction("Index", "Home", new { area = ""});
        }
    }
}
