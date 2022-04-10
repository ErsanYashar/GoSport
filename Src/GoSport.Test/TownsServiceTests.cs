using GoSport.Core.Services;
using GoSport.Core.ViewModel.Town;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace GoSport.Test
{
    public class TownsServiceTests : BaseServiceTests
    {

        [Fact]
        public void AddTownShouldReturnCorrectTown()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var service = new TownService(this.Mapper, null, context);

            var town = service.AddTown(new TownViewModel
            {
                Name = "Pleven",
                zipCode = 3000
            });

            var expectedTown = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            Assert.True(town.Name.Equals(expectedTown.Name));
            Assert.True(town.zipCode.Equals(expectedTown.zipCode));


        }

        [Fact]
        public void GetAllTownsShouldReturnCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new TownService(this.Mapper, null, context);
            context.Add(new Town 
            { 
                Name = "Haskovo",
                zipCode = 7000
            });

            context.SaveChanges();

            var result = service.GetAllTowns().Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetTownByIdShouldReturnCorrectTown()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new TownService(this.Mapper, null, context);
            context.Add(new Town { Name = "Sofia" });
            context.SaveChanges();

            var town = service.GetTownById(1);

            Assert.NotNull(town);
        }

        [Fact]
        public void IsDeleteTownShouldDeleteTown()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new TownService(this.Mapper, null, context);
            context.Add(new Town { Name = "Sofia" });
            context.SaveChanges();

            var result = service.IsDeleteTown(new TownViewModel { Id = 1 });

            Assert.True(result);
        }

        [Fact]
        public void IsDeleteTownShouldNotDeleteTown()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new TownService(this.Mapper, null, context);
            context.Add(new Town { Name = "Sofia" });
            context.SaveChanges();

            var result = service.IsDeleteTown(new TownViewModel { Id = 2 });

            Assert.False(result);
        }

        [Fact]
        public void UpdateTownShouldReturnCorrectTownViewModel()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new TownService(this.Mapper, null, context);
            context.Add(new Town { Name = "Sofia", zipCode = 2000});
            context.SaveChanges();

            var town = service.UpdateTown(new TownViewModel { Id = 1, Name = "Sofia-selo", zipCode = 2000});

            var expectedTown = new TownViewModel { Id = 1, Name = "Sofia-selo", zipCode = 2000 };

            Assert.True(town.Id.Equals(expectedTown.Id));
            Assert.True(town.Name.Equals(expectedTown.Name));
            Assert.True(town.zipCode.Equals(expectedTown.zipCode));

        }

        [Fact]
        public void UpdateTownShouldReturnNull()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new TownService(this.Mapper, null, context);
            context.Add(new Town { Name = "Sofia", zipCode = 2000 });
            context.SaveChanges();

            var town = service.UpdateTown(new TownViewModel { Id = 5, Name = "Sofia-Grad", zipCode = 2000 });

            Assert.Null(town);
        }


    }
}
