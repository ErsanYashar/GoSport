using GoSport.Core.ViewModel.Event;
using GoSport.Infrastructure.Data.DateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Core.Services.Interfaces
{
    public interface IEventsService
    {
        IEnumerable<EventViewModel> AllEvents();

        Event Add(CreateEventViewModel model);

        UpdateEventViewModel EventUpdateById(int id);
        UpdateEventViewModel UpdateEvent(UpdateEventViewModel model);
        EventViewModel GetEventById(int id);
        void DeleteEvent(EventViewModel model);
        IEnumerable<EventViewModel> AllEventsInTown(SearchTownViewModel model);

        bool IsUserParticipate(string userId, int eventId);
        bool CheckForFreeSpace(int eventId);
        EventUser JoinUserToEvent(string userId, int eventId);
        void LeaveUserFromEvent(string userId, int eventId);
    }
}
