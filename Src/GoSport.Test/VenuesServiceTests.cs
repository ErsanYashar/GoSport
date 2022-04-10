using GoSport.Core.Services;
using GoSport.Core.ViewModel.Venue;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GoSport.Test
{
    public class VenuesServiceTests : BaseServiceTests
    {
        [Fact]
        public void AddVenueShouldReturnCorrectVenue()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);

            var venue = service.AddVenue(new AddVenueViewModel
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            var expectedVenue = new Venue
            {
                Id = 1,
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            };

            Assert.True(venue.Id.Equals(expectedVenue.Id));
            Assert.True(venue.Name.Equals(expectedVenue.Name));
            Assert.True(venue.Address.Equals(expectedVenue.Address));
            Assert.True(venue.Capacity.Equals(expectedVenue.Capacity));
            Assert.True(venue.ImageVenueUrl.Equals(expectedVenue.ImageVenueUrl));
            Assert.True(venue.TownId.Equals(expectedVenue.TownId));

        }

        [Fact]
        public void AddVenueShouldReturnNoCorrectVenue()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);

            var venue = service.AddVenue(new AddVenueViewModel
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            var expectedVenue = new Venue
            {
                Id = 1,
                Name = "neshto",
                Address = "burgas",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            };

            Assert.False(venue.Name.Equals(expectedVenue.Name));
            Assert.False(venue.Address.Equals(expectedVenue.Address));          
        }

        [Fact]
        public void GetAllVenuesShouldReturnCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);
            context.Add(new Venue 
            {
                Name = "stadion",
                Address= "Varna",
                ImageVenueUrl= "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = new Town { Name = "VARNAA", zipCode = 2000}

            });

            context.SaveChanges();

            var result = service.GetAllVenues().Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetAllVenuesShouldReturnNoCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);
            context.Add(new Venue
            {
                Name = "stadion",
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
            });

            context.SaveChanges();

            var result = service.GetAllVenues().Count();

            Assert.False(1 == result);
        }

        [Fact]
        public void GetVenueByIdShouldReturnCorrectVenueViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);

            context.Add(new Town { Name = "Varna", zipCode = 2000});

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Venue",
                Address = "Varna",
                Capacity = 12,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            var venue = service.VenueById(1);

            var expectedVenue = new VenueViewModel()
            {
                Id = 1,
                Name = "Venue",
                Address = "Varna",
                Capacity = 12,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = "Varna"
            };

            Assert.True(venue.Id.Equals(expectedVenue.Id));
            Assert.True(venue.Name.Equals(expectedVenue.Name));
            Assert.True(venue.Address.Equals(expectedVenue.Address));
            Assert.True(venue.Capacity.Equals(expectedVenue.Capacity));
            Assert.True(venue.ImageVenueUrl.Equals(expectedVenue.ImageVenueUrl));
            Assert.True(venue.TownId.Equals(expectedVenue.TownId));
        }


        [Fact]
        public void GetVenueByIdShouldReturnNoCorrectVenueViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);

            context.Add(new Town { Name = "Varna", zipCode = 2000 });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Ersan",
                Address = "Varna",
                Capacity = 12,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            var venue = service.VenueById(1);

            var expectedVenue = new VenueViewModel()
            {
                Id = 1,
                Name = "Venue",
                Address = "Varna",
                Capacity = 12,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = "Varna"
            };

            Assert.False(venue.Name.Equals(expectedVenue.Name));
        }


        [Fact]
        public void UpdateVenueShouldReturnCorrectVenueViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);
            context.Add(new Town { Name = "Varna", zipCode = 2000 });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Venue",
                Capacity = 12,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            var venue = service.UpdateVenue(new VenueViewModel()
            {
                Id = 1,
                Name = "New Venue",
                Address = "Varna",
                Capacity = 15,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = "Varna"
            });

            var expectedVenue = new VenueViewModel()
            {
                Id = 1,
                Name = "New Venue",
                Address = "Varna",
                Capacity = 15,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = "Varna"
            };

            Assert.True(venue.Id.Equals(expectedVenue.Id));
            Assert.True(venue.Name.Equals(expectedVenue.Name));
            Assert.True(venue.Address.Equals(expectedVenue.Address));
            Assert.True(venue.Capacity.Equals(expectedVenue.Capacity));
            Assert.True(venue.ImageVenueUrl.Equals(expectedVenue.ImageVenueUrl));
            Assert.True(venue.TownId.Equals(expectedVenue.TownId));
        }



        [Fact]
        public void UpdateVenueShouldReturnNoCorrectVenueViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);
            context.Add(new Town { Name = "Varna", zipCode = 2000 });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Venue",
                Capacity = 12,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            var venue = service.UpdateVenue(new VenueViewModel()
            {
                Id = 1,
                Name = "New Venue",
                Address = "Varna",
                Capacity = 15,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = "Varna"
            });

            var expectedVenue = new VenueViewModel()
            {
                Id = 1,
                Name = "New Venue",
                Address = "Varna",
                Capacity = 12,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = "Varna"
            };

            Assert.False(venue.Capacity.Equals(expectedVenue.Capacity));
        }

        [Fact]
        public void GetVenueByIdShouldReturnNull()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Ersan",
                Address = "Varna",
                Capacity = 12,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            var venue = service.VenueById(1);

            var expectedVenue = new VenueViewModel()
            {
                Id = 1,
                Name = "Venue",
                Address = "Varna",
                Capacity = 12,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            };

            Assert.Null(venue);
        }

        [Fact]
        public void UpdateVenueShouldReturnNull()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Venue",
                Capacity = 13,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            var venue = service.UpdateVenue(new VenueViewModel()
            {
                Name = "Venue",
                Capacity = 13,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            Assert.Null(venue);
        }

        [Fact]
        public void DeleteVenueShouldReturnCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);
            context.Add(new Town { Name = "Varna", zipCode = 2000 });
            context.Add(new Town { Name = "Sofia", zipCode = 2200 });


            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Venue",
                Capacity = 15,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Second Venue",
                Capacity = 10,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 2
            });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Third Venue",
                Capacity = 200,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            service.DeleteVenue(new VenueViewModel() { Id = 1 });
            var result = context.Venues.Count();

            Assert.Equal(2, result);
        }


        [Fact]
        public void GetAllVenuesByTownIdShouldReturnCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);

            context.Add(new Town { Name = "Varna", zipCode = 2000 });
            context.Add(new Town { Name = "Sofia", zipCode = 2200 });


            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Venue",
                Capacity = 15,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Second Venue",
                Capacity = 10,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 2
            });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Third Venue",
                Capacity = 200,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

          var result= service.AllVenuesByTownId(1).Count();

            Assert.Equal(2, result);
        }


        [Fact]
        public void GetAllVenuesByTownIdShouldReturnNoCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new VenuesService(this.Mapper, null, context);

            context.Add(new Town { Name = "Varna", zipCode = 2000 });
            context.Add(new Town { Name = "Sofia", zipCode = 2200 });


            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Venue",
                Capacity = 15,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Second Venue",
                Capacity = 10,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 2
            });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Third Venue",
                Capacity = 200,
                Address = "Varna",
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                TownId = 1
            });

            var result = service.AllVenuesByTownId(1).Count();

            Assert.False(1 == result);
        }

    }
}
