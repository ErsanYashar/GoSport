using GoSport.Core.Services.Interfaces;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GoSport.Test
{
    public class UsersServiceTests : BaseServiceTests
    {
        [Fact]
        public void GetAllUsersShouldReturnCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new UsersService(this.Mapper, null, context);
            context.Add(new User 
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            });

            context.SaveChanges();

            var result = service.GetAllUsers().Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetAllUsersShouldReturnNoCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new UsersService(this.Mapper, null, context);
            context.Add(new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            });

            context.SaveChanges();

            var result = service.GetAllUsers().Count();

            Assert.False(2 == result);
        }


    }
}
