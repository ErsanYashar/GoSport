using AutoMapper;
using GoSport.Core.Constants;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.User;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

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
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public UsersController(UserManager<User> userManager, ApplicationDbContext dbContex, ITown townService, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IMapper mapper, IUsersService usersService)
        {
            this.userManager = userManager;
            this.dbContex = dbContex;
            this.townService = townService;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.usersService = usersService;
        }

        public IActionResult Register()
        {
            this.ViewData[ConstCore.Town] = this.townService.GetAllTownNames();
            return this.View();
        }

        //public async Task<IActionResult> AddRole()
        //{
        //    if (!await this.roleManager.RoleExistsAsync("Admin"))
        //    {
        //        await this.roleManager.CreateAsync(new IdentityRole
        //        {
        //            Name = "Admin"
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
                //this.ViewData[ConstCore.Town] = this.townService.GetAllTownNames();
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

        [Authorize]
        public IActionResult UpdateAccount()
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var model = this.mapper.Map<UpdateAccountViewModel>(user);
            this.ViewData[ConstCore.Town] = this.townService.GetAllTownNames();
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateAccount(UpdateAccountViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[ConstCore.Town] = this.townService.GetAllTownNames();
                return this.View(model);
            }

            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.BirthDate = model.BirthDate;
            user.TownId = model.TownId;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await this.userManager.UpdateAsync(user);

            this.ViewData["Message"] = ConstCore.Message;
            this.ViewData[ConstCore.Town] = this.townService.GetAllTownNames();
            return this.View(model);
        }


        [Authorize]
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var ChangedPassword = this.userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).GetAwaiter().GetResult();

            if (!ChangedPassword.Succeeded)
            {
                this.ViewData["Error"] = ConstCore.PasswordChanged;
                return this.View(model);
            }

            this.ViewData["Message"] = ConstCore.PasswordWasChanged;
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All(int? page)
        {
            var users = this.usersService.GetAllUsers();

            var pageNumber = page ?? 1;
            var usersPage = users.ToPagedList(pageNumber, 15);

            this.ViewData["Users"] = usersPage;

            return this.View();
        }
    }
}
