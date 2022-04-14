using GoSport.Core.Services;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Event;
using GoSport.Core.ViewModel.Sport;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Linq;
using Xunit;

namespace GoSport.Test
{

    public class EventsServiceTests : BaseServiceTests
    {
        private readonly ITown townsService;

        [Fact]
        public void AddEventShouldReturnCorrectEventObj()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            var Addevent = service.Add(new CreateEventViewModel
            {
                EventName = "byqgane",
                Date = DateTime.ParseExact("01-04-2022 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizerId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 11
            });

            var expectedEvent = new Event
            {
                Id = 1,
                EventName = "byqgane",
                Date = DateTime.ParseExact("01-04-2022 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizerId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 11
            };

            Assert.True(Addevent.EventName.Equals(expectedEvent.EventName));
            Assert.True(Addevent.Date.Equals(expectedEvent.Date));
            Assert.True(Addevent.Id.Equals(expectedEvent.Id));
            Assert.True(Addevent.OrganizerId.Equals(expectedEvent.OrganizerId));
            Assert.True(Addevent.DisciplineId.Equals(expectedEvent.DisciplineId));
            Assert.True(Addevent.VenueId.Equals(expectedEvent.VenueId));
            Assert.True(Addevent.NumberOfParticipants.Equals(expectedEvent.NumberOfParticipants));

        }

        [Fact]
        public void AddEventShouldReturnNoCorrectEventObj()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);
            var serviceEvent = new VenuesService(this.Mapper, null, context);

            var Addevent = service.Add(new CreateEventViewModel
            {
                EventName = "byqgane",
                Date = DateTime.ParseExact("01-04-2022 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizerId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 11
            });

            var expectedEvent = new Event
            {
                Id = 1,
                EventName = "Errrr",
                Date = DateTime.ParseExact("01-04-2022 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizerId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 11
            };

            Assert.False(Addevent.EventName.Equals(expectedEvent.EventName));
        }


        [Fact]
        public void GetAllEventsShouldReturnCorrectCount()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);
            var serviceVenue = new VenuesService(this.Mapper, null, context);

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            context.Add(new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.Now,
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.SaveChanges();

            var events = context
             .Events
             .Select(x => new EventViewModel
             {
                 Id = x.Id,
                 EventName = x.EventName,
                 Date = x.Date.ToString("dd MMMM yyyy, dddd", CultureInfo.InvariantCulture),
                 Time = x.Date.ToString("HH:mm"),
                 ImageVenueUrl = x.Venue.ImageVenueUrl,
                 Organizer = x.Organizer.Name,
                 Sport = x.Discipline.Sport.Name,
                 Discipline = x.Discipline.Name,
                 Town = x.Venue.Town.Name,
                 Venue = x.Venue.Name,
                 RealDate = x.Date,
                 NumberOfParticipants = x.NumberOfParticipants

             })
             .ToList();

            var result = service.AllEvents().Count();
            result = events.Count();

            Assert.Equal(events.Count, context.Events.Count());
            Assert.Equal(result, context.Events.Count());

        }


        [Fact]
        public void GetAllEventsShouldReturnNoCorrectCount()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);
            var serviceVenue = new VenuesService(this.Mapper, null, context);

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            context.Add(new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.Now,
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.SaveChanges();

            var events = context
             .Events
             .Select(x => new EventViewModel
             {
                 Id = x.Id,
                 EventName = x.EventName,
                 Date = x.Date.ToString("dd MMMM yyyy, dddd", CultureInfo.InvariantCulture),
                 Time = x.Date.ToString("HH:mm"),
                 ImageVenueUrl = x.Venue.ImageVenueUrl,
                 Organizer = x.Organizer.Name,
                 Sport = x.Discipline.Sport.Name,
                 Discipline = x.Discipline.Name,
                 Town = x.Venue.Town.Name,
                 Venue = x.Venue.Name,
                 RealDate = x.Date,
                 NumberOfParticipants = x.NumberOfParticipants

             })
             .ToList();

            var result = service.AllEvents().Count();
            result = events.Count();

            Assert.False(result == 0);

        }


        [Fact]
        public void GetAllEventsInTownReturnCorrectCount()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);
            var serviceVenue = new VenuesService(this.Mapper, null, context);

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            context.Add(new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.Now,
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.SaveChanges();

            var model = new SearchTownEventViewModel { TownId = 1 };

            var events = context
             .Events
             .Where(e => e.Venue.TownId == model.TownId)
             .Select(x => new EventViewModel
             {
                 Id = x.Id,
                 EventName = x.EventName,
                 Date = x.Date.ToString("dd MMMM yyyy, dddd", CultureInfo.InvariantCulture),
                 Time = x.Date.ToString("HH:mm"),
                 ImageVenueUrl = x.Venue.ImageVenueUrl,
                 Organizer = x.Organizer.Name,
                 Sport = x.Discipline.Sport.Name,
                 Discipline = x.Discipline.Name,
                 Town = x.Venue.Town.Name,
                 Venue = x.Venue.Name,
                 RealDate = x.Date,
                 NumberOfParticipants = x.NumberOfParticipants

             })
             .ToList();

            var result = service.AllEventsInTown(model).Count();
            result = events.Count();

            Assert.Equal(events.Count, context.Events.Count());
            Assert.Equal(result, context.Events.Count());
        }

        [Fact]
        public void GetAllEventsInTownReturnNoCorrectCount()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);
            var serviceVenue = new VenuesService(this.Mapper, null, context);

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            context.Add(new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.Now,
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.SaveChanges();

            var model = new SearchTownEventViewModel { TownId = 1 };

            var events = context
             .Events
             .Where(e => e.Venue.TownId == model.TownId)
             .Select(x => new EventViewModel
             {
                 Id = x.Id,
                 EventName = x.EventName,
                 Date = x.Date.ToString("dd MMMM yyyy, dddd", CultureInfo.InvariantCulture),
                 Time = x.Date.ToString("HH:mm"),
                 ImageVenueUrl = x.Venue.ImageVenueUrl,
                 Organizer = x.Organizer.Name,
                 Sport = x.Discipline.Sport.Name,
                 Discipline = x.Discipline.Name,
                 Town = x.Venue.Town.Name,
                 Venue = x.Venue.Name,
                 RealDate = x.Date,
                 NumberOfParticipants = x.NumberOfParticipants

             })
             .ToList();

            var result = service.AllEventsInTown(model).Count();
            result = events.Count();

            Assert.False(result == 0);
        }


        [Fact]
        public void GetEventByIdShouldReturnCorrectViewModel()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);
            var serviceVenue = new VenuesService(this.Mapper, null, context);

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            context.Add(new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.SaveChanges();

            var result = service.GetEventById(1);

            var expectedViewModel = new EventViewModel
            {
                EventName = "drrrr",
                Date = "20 March 2022, Sunday",
                Time = "11:00",
                NumberOfParticipants = 15
            };

            Assert.Equal(result.EventName, expectedViewModel.EventName);
            Assert.Equal(result.Date, expectedViewModel.Date);
            Assert.Equal(result.Time, expectedViewModel.Time);
            Assert.Equal(result.NumberOfParticipants, expectedViewModel.NumberOfParticipants);

        }



        [Fact]
        public void IsUserParticipateShouldReturnTrue()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            context.Users.Add(new User { FirstName = "ersan", LastName = "YUashar", UserName = "Ersan" });
            context.SaveChanges();

            context.EventUsers.Add(new EventUser
            {
                UserId = "1",
                EventId = 1
            });
            context.SaveChanges();

            var result = service.IsUserParticipate("1", 1);

            Assert.True(result);
        }

        [Fact]
        public void IsUserParticipateShouldReturnNoTrue()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            context.Users.Add(new User { FirstName = "ersan", LastName = "YUashar", UserName = "Ersan" });
            context.SaveChanges();

            context.EventUsers.Add(new EventUser
            {
                UserId = "1",
                EventId = 1
            });
            context.SaveChanges();

            var result = service.IsUserParticipate("0", 1);

            Assert.False(result);
        }

        [Fact]
        public void JoinUserToEventShouldReturnCorrectResult()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            context.Users.Add(new User { FirstName = "ersan", LastName = "YUashar", UserName = "Ersan" });

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            context.Add(new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.SaveChanges();

            var participant = service.JoinUserToEvent("1", 1);

            var expectedParticipant = new EventUser
            {
                UserId = "1",
                EventId = 1
            };

            Assert.True(participant.UserId.Equals(expectedParticipant.UserId));
            Assert.True(participant.EventId.Equals(expectedParticipant.EventId));

        }

        [Fact]
        public void JoinUserToEventShouldReturnNoCorrectResult()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            context.Users.Add(new User { FirstName = "ersan", LastName = "YUashar", UserName = "Ersan" });

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            context.Add(new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.SaveChanges();

            var participant = service.JoinUserToEvent("1", 1);

            var expectedParticipant = new EventUser
            {
                UserId = "1",
                EventId = 1
            };

            Assert.True(participant.UserId.Equals(expectedParticipant.UserId));
            Assert.True(participant.EventId.Equals(expectedParticipant.EventId));

        }

        [Fact]
        public void LeaveUserFromEventShouldReturnCorrectResult()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            context.Users.Add(new User { FirstName = "ersan", LastName = "YUashar", UserName = "Ersan" });

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });

            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            context.Add(new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.SaveChanges();

            var participant = service.JoinUserToEvent("1", 1);

            service.LeaveUserFromEvent("1", 1);
            var result = context.EventUsers.Count();

            Assert.Equal(0, result);
        }


        [Fact]

        public void GetEventsWithMyParticipationCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);
            var serviceVenue = new VenuesService(this.Mapper, null, context);

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr,
                PresidentId = "1"
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            var eventTest = new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.Now,
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            };


            var expectedParticipant = new EventUser
            {
                User = userr,
                Event = eventTest
            };
            context.EventUsers.Add(expectedParticipant);

            context.SaveChanges();

            var participantList = context
              .EventUsers
              .Where(p => p.User.UserName == "ersan")
               .OrderBy(p => p.Event.Date)
              .Select(x => new MyEventViewModel
              {
                  EventName = x.Event.EventName,
                  Date = x.Event.Date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                  Time = x.Event.Date.ToString("HH:mm"),
                  Sport = x.Event.Discipline.Sport.Name,
                  Discipline = x.Event.Discipline.Name,
                  Town = x.Event.Venue.Town.Name,
                  Venue = x.Event.Venue.Name,
                  RemainingTime = $"{Math.Ceiling((x.Event.Date - DateTime.UtcNow).TotalDays)}{"days"}"
              })
             .ToList();

            var result = service.GetEventsWithMyParticipation("ersan").Count();
            result = participantList.Count();

            Assert.Equal(1, result);

        }

        [Fact]
        public void GetEventsWithMyParticipationNoCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);
            var serviceVenue = new VenuesService(this.Mapper, null, context);

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr,
                PresidentId = "1"
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            var eventTest = new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.Now,
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            };


            var expectedParticipant = new EventUser
            {
                User = userr,
                Event = eventTest
            };
            context.EventUsers.Add(expectedParticipant);

            context.SaveChanges();

            var participantList = context
              .EventUsers
              .Where(p => p.User.UserName == "ersan")
               .OrderBy(p => p.Event.Date)
              .Select(x => new MyEventViewModel
              {
                  EventName = x.Event.EventName,
                  Date = x.Event.Date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                  Time = x.Event.Date.ToString("HH:mm"),
                  Sport = x.Event.Discipline.Sport.Name,
                  Discipline = x.Event.Discipline.Name,
                  Town = x.Event.Venue.Town.Name,
                  Venue = x.Event.Venue.Name,
                  RemainingTime = $"{Math.Ceiling((x.Event.Date - DateTime.UtcNow).TotalDays)}{"days"}"
              })
             .ToList();

            var result = service.GetEventsWithMyParticipation("ersan").Count();
            result = participantList.Count();

            Assert.False(0 == result);

        }

        [Fact]
        public void EventUpdateByIdShouldReturnCorrectEventViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr,
                PresidentId = "1"
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            context.Events.Add(new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.Events.Add(new Event
            {
                EventName = "ersan",
                NumberOfParticipants = 10,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.SaveChanges();

            var eventT = service.EventUpdateById(1);

            var expectedViewModel = new UpdateEventViewModel()
            {
                Id = 1,
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                VenueId = venue.Id,
                DisciplineId = discipline.Id,
                OrganizerId = organi.Id
            };

            Assert.True(eventT.EventName.Equals(expectedViewModel.EventName));
            Assert.True(eventT.Id.Equals(expectedViewModel.Id));
            Assert.True(eventT.NumberOfParticipants.Equals(expectedViewModel.NumberOfParticipants));
            Assert.True(eventT.Date.Equals(expectedViewModel.Date));
            Assert.True(eventT.VenueId.Equals(expectedViewModel.VenueId));
            Assert.True(eventT.DisciplineId.Equals(expectedViewModel.DisciplineId));
            Assert.True(eventT.OrganizerId.Equals(expectedViewModel.OrganizerId));
        }

        [Fact]
        public void EventUpdateByIdShouldReturnNoCorrectEventViewModel()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr,
                PresidentId = "1"
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            context.Events.Add(new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.Events.Add(new Event
            {
                EventName = "ersan",
                NumberOfParticipants = 10,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.SaveChanges();

            var eventT = service.EventUpdateById(2);

            var expectedViewModel = new UpdateEventViewModel()
            {
                Id = 1,
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                VenueId = venue.Id,
                DisciplineId = discipline.Id,
                OrganizerId = organi.Id
            };

            Assert.False(eventT.Id.Equals(expectedViewModel.Id));
        }

        [Fact]
        public void UpdateEventShouldReturnCorrectUpdate()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr,
                PresidentId = "1"
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            context.Events.Add(new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.Events.Add(new Event
            {
                EventName = "ersan",
                NumberOfParticipants = 10,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.SaveChanges();

            var udateEvent = new UpdateEventViewModel()
            {
                Id = 1,
                NumberOfParticipants = 6,
                EventName = "drrrr",
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                VenueId = venue.Id,
                DisciplineId = discipline.Id,
                OrganizerId = organi.Id
            };

            var eventT = service.UpdateEvent(udateEvent);

            var expectedViewModel = new UpdateEventViewModel()
            {
                Id = 1,
                EventName = "drrrr",
                NumberOfParticipants = 6,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                VenueId = venue.Id,
                DisciplineId = discipline.Id,
                OrganizerId = organi.Id
            };

            Assert.True(eventT.NumberOfParticipants.Equals(expectedViewModel.NumberOfParticipants));
        }


        [Fact]
        public void UpdateEventShouldReturnNul()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            var town = new Town
            {
                Name = "Pleven",
                zipCode = 3000
            };

            var venue = new Venue
            {
                Name = "Stadion",
                Address = "varna",
                Capacity = 10,
                ImageVenueUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                Town = town
            };

            var userr = new User
            {
                UserName = "ersan",
                Email = "ersan.mehmed@abv.bg",
                FirstName = "Ersan",
                LastName = "Yashar",
                TownId = 1,
                BirthDate = DateTime.Now,
            };

            var organi = new Organizer
            {
                Name = "Test",
                Description = "Description",
                President = userr,
                PresidentId = "1"
            };

            context.Organizers.Add(organi);

            var serviceSport = new SportsService(this.Mapper, null, context);

            var sport = serviceSport.Add(new AddSportViewModel
            {
                Name = "Fitness",
                Description = "Body",
                ImageSportUrl = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG"
            });


            var discipline = new Discipline
            {
                Sport = sport,
                Name = "Tejesti",
                Description = "razvitie"
            };

            context.Disciplines.Add(discipline);

            context.Events.Add(new Event
            {
                EventName = "drrrr",
                NumberOfParticipants = 15,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.Events.Add(new Event
            {
                EventName = "ersan",
                NumberOfParticipants = 10,
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Venue = venue,
                Discipline = discipline,
                Organizer = organi
            });

            context.SaveChanges();

            var udateEvent = new UpdateEventViewModel()
            {
                NumberOfParticipants = 15,
                EventName = "drrrr",
                Date = DateTime.ParseExact("20-03-2022 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                VenueId = venue.Id,
                DisciplineId = discipline.Id,
                OrganizerId = organi.Id
            };

            var eventT = service.UpdateEvent(udateEvent);

            Assert.Null(eventT);
        }

        [Fact]
        public void DeleteEventShouldReturnCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            service.Add(new CreateEventViewModel
            {
                EventName = "byqgane",
                Date = DateTime.ParseExact("01-04-2022 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizerId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 11
            });

            service.Add(new CreateEventViewModel
            {
                EventName = "byqgane",
                Date = DateTime.ParseExact("01-04-2022 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizerId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 11
            });

            service.DeleteEvent(new EventViewModel { Id = 1 });
            var result = context.Events.Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public void CheckForFreeSpaceShouldReturtTrue()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            service.Add(new CreateEventViewModel
            {
                EventName = "byqgane",
                Date = DateTime.ParseExact("01-04-2022 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizerId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 3
            });

            context.Users.Add(new User {Id = "test1", FirstName = "ersan", LastName = "YUashar", UserName = "Ersan" });
            context.Users.Add(new User {Id = "test2", FirstName = "Emir", LastName = "Yashar", UserName = "emir" });

            context.EventUsers.Add(new EventUser
            {
                EventId = 1,
                UserId = "test1"
            });

            context.EventUsers.Add(new EventUser
            {
                EventId = 1,
                UserId = "test2"
            });

            context.SaveChanges();

            var result = service.CheckForFreeSpace(1);

            Assert.True(result);
        }

        [Fact]
        public void CheckForFreeSpaceShouldReturtFalse()
        {

            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new EventsService(this.Mapper, null, context, townsService);

            service.Add(new CreateEventViewModel
            {
                EventName = "byqgane",
                Date = DateTime.ParseExact("01-04-2022 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizerId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 2
            });

            context.Users.Add(new User { Id = "test1", FirstName = "ersan", LastName = "YUashar", UserName = "Ersan" });
            context.Users.Add(new User { Id = "test2", FirstName = "Emir", LastName = "Yashar", UserName = "emir" });

            context.EventUsers.Add(new EventUser
            {
                EventId = 1,
                UserId = "test1"
            });

            context.EventUsers.Add(new EventUser
            {
                EventId = 1,
                UserId = "test2"
            });

            context.SaveChanges();

            var result = service.CheckForFreeSpace(1);

            Assert.False(result);
        }
    }
}

