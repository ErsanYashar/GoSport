using GoSport.Core.Services;
using GoSport.Core.ViewModel.Organizer;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace GoSport.Test
{
    public class OrganizersServiceTests : BaseServiceTests
    {
        [Fact]
        public void AddOrganizesShouldReturnCorrect()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var service = new OrganizersService(this.Mapper, userManager, context);
            userManager.CreateAsync(new User { UserName = "Ersan", FirstName = "Ersann", LastName = "Yashar" }
            , "Aa123456!").GetAwaiter().GetResult();

            var organizer = service.Add(new AddOrganizerViewModel
            {
                Name = "Test",
                Description = "Description"
            }, "Ersan");

            var expectedOrganization = new Organizer
            {
                Id = 1,
                Name = "Test",
                Description = "Description",
            };

            Assert.True(organizer.Name.Equals(expectedOrganization.Name));
            Assert.True(organizer.Id.Equals(expectedOrganization.Id));
            Assert.True(organizer.Description.Equals(expectedOrganization.Description));
        }

        [Fact]
        public void AddOrganizesShouldReturnNoCorrect()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var service = new OrganizersService(this.Mapper, userManager, context);
            userManager.CreateAsync(new User { UserName = "Ersan", FirstName = "Ersann", LastName = "Yashar" }
            , "Aa123456!").GetAwaiter().GetResult();

            var organization = service.Add(new AddOrganizerViewModel
            {
                Name = "Ersan",
                Description = "Description"
            }, "Ersan");

            var expectedOrganization = new Organizer
            {
                Id = 1,
                Name = "Test",
                Description = "Description",
            };

            Assert.False(organization.Name.Equals(expectedOrganization.Name));
        }

        [Fact]
        public void GetAllOrganizersShouldReturnCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new OrganizersService(this.Mapper, null, context);
            context.Organizers.Add(new Organizer { Name = "Ersan", Description = "Futbolist", PresidentId = "1"});
            context.Organizers.Add(new Organizer { Name = "figyan", Description = "grupovi trenirovki", PresidentId ="2" });
            context.SaveChanges();

            var result = service.AllOrganizers().Count();

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetAllOrganizersShouldReturnNoCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new OrganizersService(this.Mapper, null, context);
            context.Organizers.Add(new Organizer { Name = "Ersan", Description = "Futbolist", PresidentId = "1" });
            context.Organizers.Add(new Organizer { Name = "figyan", Description = "grupovi trenirovki", PresidentId = "2" });
            context.SaveChanges();

            var result = service.AllOrganizers().Count();

            Assert.False(1 == result);
        }

        [Fact]
        public void GetOrganizerByIdShouldReturnCorrectOrganizerViewModel()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var service = new OrganizersService(this.Mapper, userManager, context);
            userManager.CreateAsync(new User { UserName = "Ersan", FirstName = "Ersann", LastName = "Yashar" }
            , "Aa123456!").GetAwaiter().GetResult();

            service.Add(new AddOrganizerViewModel()
            {
                Name = "First",
                Description = "Test",
            }, "Ersan");

            var organization = service.organizerById(1);

            var expectedOrganization = new Organizer()
            {
                Id = 1,
                Name = "First",
                Description = "Test"
            };

            Assert.True(organization.Name.Equals(expectedOrganization.Name));
            Assert.True(organization.Id.Equals(expectedOrganization.Id));
            Assert.True(organization.Description.Equals(expectedOrganization.Description));
        }

        [Fact]
        public void GetOrganizerByIdShouldReturnNoCorrectOrganizerViewModel()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var service = new OrganizersService(this.Mapper, userManager, context);
            userManager.CreateAsync(new User { UserName = "Ersan", FirstName = "Ersann", LastName = "Yashar" }
            , "Aa123456!").GetAwaiter().GetResult();

            service.Add(new AddOrganizerViewModel()
            {
                Name = "First",
                Description = "Test",
            }, "Ersan");

            var organization = service.organizerById(1);

            var expectedOrganization = new Organizer()
            {
                Id = 1,
                Name = "Firs",
                Description = "Test"
            };

            Assert.False(organization.Name.Equals(expectedOrganization.Name));

        }

        [Fact]
        public void DeleteOrganizerShouldReturnCorrectCount()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var service = new OrganizersService(this.Mapper, userManager, context);
            userManager.CreateAsync(new User { UserName = "Ersan", FirstName = "Ersann", LastName = "Yashar" }
            , "Aa123456!").GetAwaiter().GetResult();
            userManager.CreateAsync(new User { UserName = "Figyan", FirstName = "Ersan", LastName = "Yashar" }
            ,"Aa1234567!").GetAwaiter().GetResult();

            service.Add(new AddOrganizerViewModel()
            {
                Name = "First",
                Description = "Description",
            }, "Ersan");

            service.Add(new AddOrganizerViewModel()
            {
                Name = "First",
                Description = "Description",
            }, "Figyan");

            service.DeleteOrganization(new OrganizerViewModel() { Id = 1 });
            var result = context.Organizers.Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public void DeleteOrganizerShouldReturnNoCorrectCount()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var service = new OrganizersService(this.Mapper, userManager, context);
            userManager.CreateAsync(new User { UserName = "Ersan", FirstName = "Ersann", LastName = "Yashar" }
            , "Aa123456!").GetAwaiter().GetResult();
            userManager.CreateAsync(new User { UserName = "Figyan", FirstName = "Ersan", LastName = "Yashar" }
            , "Aa1234567!").GetAwaiter().GetResult();

            service.Add(new AddOrganizerViewModel()
            {
                Name = "First",
                Description = "Description",
            }, "Ersan");

            service.Add(new AddOrganizerViewModel()
            {
                Name = "First",
                Description = "Description",
            }, "Figyan");

            service.DeleteOrganization(new OrganizerViewModel() { Id = 1 });
            var result = context.Organizers.Count();

            Assert.False(2 == result);
        }
    }
}
