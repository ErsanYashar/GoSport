using AutoMapper;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Event;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Core.Services
{
    public class EventsService : BaseService, IEventsService
    {

        private readonly ITown townsService;
        public EventsService(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context, ITown townsService)
            : base(mapper, userManager, context)
        {
            this.townsService = townsService;
        }

        public Event Add(CreateEventViewModel model)
        {
            var eventModel = this.Mapper.Map<Event>(model);

            this.Context.Events.Add(eventModel);
            this.Context.SaveChanges();

            return eventModel;
        }

        public IEnumerable<EventViewModel> AllEvents()
        {
            var events = this.Context
                .Events
                .Select(x => new EventViewModel
                {
                    Id=x.Id,
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
                
            return events;
        }

        public IEnumerable<EventViewModel> AllEventsInTown(SearchTownViewModel model)
        {
            
            var events = this.Context
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

            return events;
        }

        public bool CheckForFreeSpace(int eventId)
        {
            var userEvent = this.Context
               .Events
               .FirstOrDefault(e => e.Id == eventId);

            var participants = this.Context
                .EventUsers
                .Where(e => e.EventId == eventId)
                .Count();

            return userEvent.NumberOfParticipants > participants;
        }

        public void DeleteEvent(EventViewModel model)
        {
            var eventDelete = this.Context
              .Events
              .FirstOrDefault(d => d.Id == model.Id);

            if (eventDelete != null)
            {
                this.Context.Events.Remove(eventDelete);
                this.Context.SaveChanges();
            }
        }

        public UpdateEventViewModel EventUpdateById(int id)
        {
            var eventUpdate = this.Context
               .Events
               .FirstOrDefault(e => e.Id == id);

            var eventViewModel = this.Mapper.Map<UpdateEventViewModel>(eventUpdate);

            return eventViewModel;
        }

        public EventViewModel GetEventById(int id)
        {
            var getEvent = this.Context
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
              .FirstOrDefault(e => e.Id == id);

            return getEvent;
        }

        public IEnumerable<MyEventViewModel> GetEventsWithMyParticipation(string username)
        {
            var participant = this.Context
              .EventUsers
              .Where(p => p.User.UserName == username && p.Event.Date >= DateTime.UtcNow)
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

            return participant;
        }

        public bool IsUserParticipate(string userId, int eventId)
        {
            var isUserParticipate = this.Context.EventUsers.Any(p => p.UserId == userId && p.EventId == eventId);
            return isUserParticipate;
        }

        public EventUser JoinUserToEvent(string userId, int eventId)
        {
            var participant = new EventUser
            {
                UserId = userId,
                EventId = eventId
            };

            this.Context.EventUsers.Add(participant);
            this.Context.SaveChanges();

            return participant;
        }

        public void LeaveUserFromEvent(string userId, int eventId)
        {
            var participant = this.Context
               .EventUsers
               .FirstOrDefault(p => p.UserId == userId && p.EventId == eventId);

            this.Context.Remove(participant);
            this.Context.SaveChanges();
        }

        public UpdateEventViewModel UpdateEvent(UpdateEventViewModel model)
        {
            var updateEvent = this.Context
             .Events
             .FirstOrDefault(e => e.Id == model.Id);

            if (updateEvent == null)
            {
                return null;
            }

            updateEvent.EventName = model.EventName;
            updateEvent.Date = model.Date;
            updateEvent.OrganizerId = model.OrganizerId;
            updateEvent.DisciplineId = model.DisciplineId;
            updateEvent.VenueId = model.VenueId;
            updateEvent.NumberOfParticipants = model.NumberOfParticipants;
            this.Context.SaveChanges();

            var eventViewModel = this.Mapper.Map<UpdateEventViewModel>(updateEvent);

            return eventViewModel;
        }
    }
}
