﻿using AutoMapper;
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
        public EventsService(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context) 
            : base(mapper, userManager, context)
        {
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
                

            //var eventsView = this.Mapper.Map<IList<Event>, IEnumerable<EventViewModel>>(events);

            return events;
        }

        public UpdateEventViewModel EventUpdateById(int id)
        {
            var eventUpdate = this.Context
               .Events
               .FirstOrDefault(e => e.Id == id);

            var eventViewModel = this.Mapper.Map<UpdateEventViewModel>(eventUpdate);

            return eventViewModel;
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
