
using AutoMapper;
using GoSport.Core.AutoMapper;
using GoSport.Core.Services;
using GoSport.Core.Services.Interfaces;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GoSport.Test
{
    public class BaseServiceTests
    {
        protected BaseServiceTests()
        {
            var efServiceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
            services.AddOptions();
            services.AddDbContext<ApplicationDbContext>(b => b.UseInMemoryDatabase("ApplicationDbContext").UseInternalServiceProvider(efServiceProvider));
            services.AddLogging();
            services.AddIdentity<User, IdentityRole>()
          .AddDefaultTokenProviders()
          .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddDefaultIdentity<User>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequiredLength = 5;
            //    options.Password.RequiredUniqueChars = 0;
            //    options.User.RequireUniqueEmail = false;
            //});
            //  services.AddRoles<IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>();
            //  services.AddControllersWithViews();

            services.AddTransient<ITown, TownService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ISportsService, SportsService>();
            services.AddTransient<IDisciplinesService, DisciplinesService>();
            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<IOrganizersService, OrganizersService>();
            services.AddTransient<IVenuesService, VenuesService>();
            services.AddTransient<IEventsService, EventsService>();

            var context = new DefaultHttpContext();

            services.AddSingleton<IHttpContextAccessor>(
               new HttpContextAccessor()
               {
                   HttpContext = context,
               });

            this.ServiceProvider = services.BuildServiceProvider();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new AMapper()));
            this.Mapper = mapperConfig.CreateMapper();
        }

        public IServiceProvider ServiceProvider { get; set; }
        public IMapper Mapper { get; set; }
    }
}
