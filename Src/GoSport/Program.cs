using AutoMapper;
using GoSport.Core.AutoMapper;
using GoSport.Core.Services;
using GoSport.Core.Services.Interfaces;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient<ITown, TownService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<ISportsService, SportsService>();
builder.Services.AddTransient<IDisciplinesService, DisciplinesService>();
builder.Services.AddTransient<IMessagesService, MessagesService>();
builder.Services.AddTransient<IOrganizersService, OrganizersService>();
builder.Services.AddTransient<IVenuesService, VenuesService>();
builder.Services.AddTransient<IEventsService, EventsService>();




builder.Services.AddDefaultIdentity<User>(options =>
   {
       options.SignIn.RequireConfirmedAccount = false;
       options.Password.RequireLowercase = false;
       options.Password.RequireUppercase = false;
       options.Password.RequireNonAlphanumeric = false;
       options.Password.RequiredLength = 5;
       options.Password.RequiredUniqueChars = 0;
       options.User.RequireUniqueEmail = false;
   })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();



builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Identity/Users/SignIn");
    options.AccessDeniedPath = new PathString("/Home/Access");
});


var mapperConfig = new MapperConfiguration(m => m.AddProfile(new AMapper()));
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "areas",
//    pattern: "user/{*register}",
//    defaults: new { controller = "User", action = "Register" });

app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapAreaControllerRoute(
//    name: "areas",
//    areaName: "Identity",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");



app.MapRazorPages();

app.Run();
