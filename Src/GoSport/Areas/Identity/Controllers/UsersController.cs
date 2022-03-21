using GoSport.Core.Constants;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.User;
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
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext dbContex;
        private readonly ITown townService;

        public UsersController(UserManager<User> userManager, ApplicationDbContext dbContex, ITown townService, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.dbContex = dbContex;
            this.townService = townService;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
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

        //public async Task<IActionResult> AddRole()
        //{
        //    if (!await this.roleManager.RoleExistsAsync("User"))
        //    {
        //        await this.roleManager.CreateAsync(new IdentityRole
        //        {
        //            Name = "User"
        //        });
        //    }

        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[ConstCore.Town] = this.townService.GetAllTownNames();
                return this.View(model);
            }

            //var user = dbContex.Users
            //    .Select(x => new User
            //    {
            //        Username = model.Username,
            //        Password = model.Password,
            //        ConfirmPassword = model.ConfirmPassword,
            //        BirthDate = model.BirthDate,
            //        FirstName = model.FirstName,
            //        LastName = model.LastName,
            //        Email = model.Email,
            //        TownId = model.TownId,
            //    }).FirstOrDefault();

            var user = new User
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                BirthDate = model.BirthDate,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (this.userManager.Users.Count() == 1)
                {
                    await this.userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, "User");
                }

                await this.signInManager.SignInAsync(user, false);
            }
            else
            {
                this.ViewData["Error"] = ConstCore.UsernameEror;
                this.ViewData[ConstCore.Town] = this.townService.GetAllTownNames();
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
