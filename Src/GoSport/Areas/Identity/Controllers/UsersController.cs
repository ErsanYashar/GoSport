using GoSport.Core.Constants;
using GoSport.Core.Services.Interfaces;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoSport.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class UsersController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext dbContex;
        private readonly ITown townService;

        public UsersController(UserManager<User> userManager, ApplicationDbContext dbContex, ITown townService)
        {
            this.userManager = userManager;
            this.dbContex = dbContex;
            this.townService = townService;
        }


        public IActionResult Index()
        {
            
            return this.View();
        }

        public IActionResult Register()
        {
            this.ViewData[ConstCore.Town] = this.townService.GetAllTownNames();
            return this.View();
        }
    }
}
