using GoSport.Core.ViewModel.Event;
using GoSport.Core.ViewModel.Venue;
using GoSport.Infrastructure.Data.DateModels;

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
        IEnumerable<EventViewModel> AllEventsInTown(SearchTownEventViewModel model);

        bool IsUserParticipate(string userId, int eventId);
        bool CheckForFreeSpace(int eventId);
        EventUser JoinUserToEvent(string userId, int eventId);
        void LeaveUserFromEvent(string userId, int eventId);
        IEnumerable<MyEventViewModel> GetEventsWithMyParticipation(string username);
    }
}
