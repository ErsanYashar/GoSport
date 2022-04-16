using GoSport.Core.Services;
using GoSport.Core.ViewModel.Discipline;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace GoSport.Test
{
    public class DisciplinesServiceTests : BaseServiceTests
    {
        [Fact]
        public void GetGetAllDisciplinesShouldReturnCorrectCount()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);
            var sport = new Sport { Name = "Fitness", Description = "Body", ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG" };

            context.Add(new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie",
            });

            context.SaveChanges();

            var result = service.GetAllDisciplines().Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetGetAllDisciplinesShouldReturnNoCorrectCount()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);
            var sport = new Sport { Name = "Fitness", Description = "Body", ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG" };

            context.Add(new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie",
            });

            context.SaveChanges();

            var result = service.GetAllDisciplines().Count();

            Assert.False(0 == result);
        }


        [Fact]
        public void AddDisciplineShouldReturnCorrectDiscipline()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);
            var sport = new Sport { Name = "Fitness", Description = "Body", ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG" };

            var discipline = service.AddDiscipline(new AddDisciplineViewModel
            {
                Name = "Test",
                Description = "Description",
                SportId = 1
            });

            var expectedDiscipline = new Discipline
            {
                Id = 1,
                Name = "Test",
                Description = "Description"
            };

            Assert.True(discipline.Name.Equals(expectedDiscipline.Name));
            Assert.True(discipline.Description.Equals(expectedDiscipline.Description));
            Assert.True(discipline.Id.Equals(expectedDiscipline.Id));
        }

        [Fact]
        public void AddDisciplineShouldReturnNoCorrectDiscipline()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);
            var sport = new Sport { Name = "Fitness", Description = "Body", ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG" };

            var discipline = service.AddDiscipline(new AddDisciplineViewModel
            {
                Name = "Test",
                Description = "Description",
                SportId = 1
            });

            var expectedDiscipline = new Discipline
            {
                Id = 1,
                Name = "Ersan",
                Description = "Description"
            };

            Assert.False(discipline.Name.Equals(expectedDiscipline.Name));

        }


        [Fact]
        public void GetDisciplinesBySportIdShouldReturnCorrectDiscipline()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);
            var sport = new Sport { Name = "Fitness", Description = "Body", ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG" };

            service.AddDiscipline(new AddDisciplineViewModel
            {
                Name = "Test",
                Description = "Description",
                SportId = 1
            });

            service.AddDiscipline(new AddDisciplineViewModel
            {
                Name = "Test",
                Description = "Description 1",
                SportId = 1
            });

            var result = service.GetDisciplinesBySportId(1).Count();

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetDisciplinesBySportIdShouldReturnNoCorrectDiscipline()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);
            var sport = new Sport { Name = "Fitness", Description = "Body", ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG" };

            service.AddDiscipline(new AddDisciplineViewModel
            {
                Name = "Test",
                Description = "Description",
                SportId = 1
            });

            service.AddDiscipline(new AddDisciplineViewModel
            {
                Name = "Test 1",
                Description = "Description 1",
                SportId = 1
            });

            var result = service.GetDisciplinesBySportId(1).Count();

            Assert.False(1 == result);
        }


        [Fact]
        public void GetDisciplineByIdShouldReturnCorrectDisciplineViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);
            var sport = new Sport { Name = "Fitness", Description = "Body", ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG" };
            context.Sports.Add(sport);

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "fitnes",
                Description = "Description",
                SportId = 1,

            });

            var discipline = service.GetDisciplineById(1);

            var expectedDiscipline = new DisciplineViewModel()
            {
                Id = 1,
                Name = "fitnes",
                Description = "Description",
                SportName = "Fitness"
            };

            Assert.True(discipline.Name.Equals(expectedDiscipline.Name));
            Assert.True(discipline.Id.Equals(expectedDiscipline.Id));
            Assert.True(discipline.Description.Equals(expectedDiscipline.Description));
            Assert.True(discipline.SportName.Equals(expectedDiscipline.SportName));

        }

        [Fact]
        public void GetDisciplineByIdShouldReturnNoCorrectDisciplineViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);
            var sport = new Sport { Name = "Fitness", Description = "Body", ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG" };
            context.Sports.Add(sport);

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "fitnes",
                Description = "Description",              
                SportId = 1,
                
            });

            var discipline = service.GetDisciplineById(1);

            var expectedDiscipline = new DisciplineViewModel()
            {
                Id = 1,
                Name = "Futboll",
                Description = "Description",
                SportName = "Fitness"
            };

            Assert.False(discipline.Name.Equals(expectedDiscipline.Name));
        }

        [Fact]
        public void UpdateDisciplineShouldReturnCorrectDisciplineViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "Test",
                Description = "Description",
                SportId = 1
            });

            var discipline = service.UpdateDiscipline(new DisciplineViewModel
            {
                Id = 1,
                Name = "Test",
                Description = "New Description",
                SportId = 1
            });

            var expectedDiscipline = new DisciplineViewModel
            {
                Id = 1,
                Name = "Test",
                Description = "New Description",
                SportId = 1
            };

            Assert.True(discipline.Description.Equals(expectedDiscipline.Description));
        }

        [Fact]
        public void UpdateDisciplineShouldReturnNoCorrectDisciplineViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "Test",
                Description = "Description",
                SportId = 1
            });

            var discipline = service.UpdateDiscipline(new DisciplineViewModel
            {
                Id = 1,
                Name = "Test",
                Description = "Description",
                SportId = 1
            });

            var expectedDiscipline = new DisciplineViewModel
            {
                Id = 1,
                Name = "Test",
                Description = "New Description",
                SportId = 1
            };

            Assert.False(discipline.Description.Equals(expectedDiscipline.Description));
        }

        [Fact]
        public void UpdateDisciplineShouldReturnNull()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "Test",
                Description = "Test Description",
                SportId = 1
            });

            var discipline = service.UpdateDiscipline(new DisciplineViewModel
            {
                Id = 2,
                Name = "New Discipline",
                Description = "New Description",
                SportId = 2
            });

            Assert.Null(discipline);
        }

        [Fact]
        public void DeleteDisciplineShouldReturnCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);
            var sport = new Sport { Name = "Fitness", Description = "Body", ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG" };
            context.Sports.Add(sport);

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "Fitnes",
                Description = "Test",
                SportId = 1
            });

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "Fitnes",
                Description = "Description",
                SportId = 1
            });

            service.DeleteDiscipline(new DisciplineViewModel { Id = 1 });
            var result = context.Disciplines.Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public void DeleteDisciplineShouldReturnNoCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new DisciplinesService(this.Mapper, null, context);
            var sport = new Sport { Name = "Fitness", Description = "Body", ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG" };
            context.Sports.Add(sport);

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "Fitnes",
                Description = "Test",
                SportId = 1
            });

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "Fitnes",
                Description = "Description",
                SportId = 1
            });

            service.DeleteDiscipline(new DisciplineViewModel { Id = 1 });
            var result = context.Disciplines.Count();

            Assert.False(2 == result);
        }
    }
}
