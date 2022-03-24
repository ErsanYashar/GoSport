using AutoMapper;
using GoSport.Core.Constants;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.User;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace GoSport.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class UsersController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext dbContex;
        private readonly ITown townService;
        private readonly IMapper mapper;

        public UsersController(UserManager<User> userManager, ApplicationDbContext dbContex, ITown townService, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.dbContex = dbContex;
            this.townService = townService;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
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

            var user = this.mapper.Map<User>(model);

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

        public IActionResult SignIn()
        {          
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var signIn = await this.signInManager.PasswordSignInAsync(model.Username, model.Password, true, true);

            if (!signIn.Succeeded)
            {
                this.ViewData["Error"] = ConstCore.UserOrPasInv;
                this.ViewData[ConstCore.Town] = this.townService.GetAllTownNames();
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await this.signInManager.SignOutAsync();
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
