using GoSport.Core.Services;
using GoSport.Core.Services.Interfaces;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient<ITown, TownService>();

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
   name: "Area",
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
