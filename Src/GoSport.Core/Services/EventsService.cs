using AutoMapper;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Event;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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

        public IEnumerable<EventViewModel> AllEvents()
        {
            var events = this.Context
                .Events
                .OrderBy(e => e.Date)
                .ToList();

            var eventsView = this.Mapper.Map<IList<Event>, IEnumerable<EventViewModel>>(events);

            return eventsView;
        }
    }
}
