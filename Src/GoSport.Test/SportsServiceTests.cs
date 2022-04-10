using GoSport.Core.Services;
using GoSport.Core.ViewModel.Sport;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace GoSport.Test
{
    public class SportsServiceTests : BaseServiceTests
    {
        [Fact]
        public void GetAllSportsShouldReturnCorrectCount()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new SportsService(this.Mapper, null, context);
            context.Add(new Sport { Name = "Fitness", Description = "Body", ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG" });
            context.SaveChanges();

            var result = service.GetAllSports().Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetAllSportsShouldReturnNoCorrectCount()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new SportsService(this.Mapper, null, context);
            context.Add(new Sport { Name = "Fitness", Description = "Body", ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG" });
            context.SaveChanges();

            var result = service.GetAllSports().Count();

            Assert.False(2 == result);
        }

        [Fact]
        public void AddSportShouldShouldReturnCorrectSport()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new SportsService(this.Mapper, null, context);

            var sport = service.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });

            var expectedSport = new Sport
            {
                Id = 1,
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            };

            Assert.True(sport.Id.Equals(expectedSport.Id));
            Assert.True(sport.Name.Equals(expectedSport.Name));
            Assert.True(sport.Description.Equals(expectedSport.Description));
            Assert.True(sport.ImageSportUrl.Equals(expectedSport.ImageSportUrl));
        }

        [Fact]
        public void AddSportShouldShouldReturnNoCorrectSport()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new SportsService(this.Mapper, null, context);

            var sport = service.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });

            var expectedSport = new Sport
            {
                Id = 1,
                Name = "Futboll",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            };

            Assert.False(sport.Name.Equals(expectedSport.Name));
            
        }

        [Fact]
        public void GetSportByIdShouldReturnCorrectSportViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new SportsService(this.Mapper, null, context);

            service.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });

            var sport = service.GetSportById(1);

            var expectedSport = new Sport
            {
                Id = 1,
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            };

            Assert.True(sport.Id.Equals(expectedSport.Id));
            Assert.True(sport.Name.Equals(expectedSport.Name));
            Assert.True(sport.Description.Equals(expectedSport.Description));
            Assert.True(sport.ImageSportUrl.Equals(expectedSport.ImageSportUrl));
        }

        [Fact]
        public void GetSportByIdShouldReturnNoCorrectSportViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new SportsService(this.Mapper, null, context);

            service.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });

            var sport = service.GetSportById(1);

            var expectedSport = new Sport
            {
                Id = 1,
                Name = "Hanball",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            };

            Assert.False(sport.Name.Equals(expectedSport.Name));
        }

        [Fact]
        public void UpdateSportShouldReturnCorrectSportViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new SportsService(this.Mapper, null, context);

            service.Add(new AddSportViewModel
            {
                Name = "Futboll",
                Description = "Kardio",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });

            var sport = service.UpdateSport(new SportViewModel { Id = 1, Name = "Sport", Description = "Description" });

            var expectedSport = new SportViewModel { Id = 1, Name = "Sport", Description = "Description" };

            Assert.True(sport.Id.Equals(expectedSport.Id));
            Assert.True(sport.Name.Equals(expectedSport.Name));
            Assert.True(sport.Description.Equals(expectedSport.Description));
        }

        [Fact]
        public void UpdateSportShouldReturnNoCorrectSportViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new SportsService(this.Mapper, null, context);

            service.Add(new AddSportViewModel
            {
                Name = "Futboll",
                Description = "Kardio",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });

            var sport = service.UpdateSport(new SportViewModel { Id = 1, Name = "Futboll", Description = "Description" });

            var expectedSport = new SportViewModel { Id = 1, Name = "Sport", Description = "Description" };

            Assert.False(sport.Name.Equals(expectedSport.Name));
        }

        [Fact]
        public void UpdateSportShouldReturnNull()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new SportsService(this.Mapper, null, context);

            service.Add(new AddSportViewModel
            {
                Name = "Futboll",
                Description = "Kardio",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });

            var sport = service.UpdateSport(new SportViewModel { Id = 5, Name = "Sport", Description = "Description" });

            Assert.Null(sport);
        }
    }
}
