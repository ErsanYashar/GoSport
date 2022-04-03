using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Display(Name = "Date and Time of Events")]
        public string Date { get; set; }

        public string Time { get; set; }

        public string Organizer { get; set; }

        public string Sport { get; set; }

        public string Discipline { get; set; }

        public string Town { get; set; }

        public string Venue { get; set; }

        public string ImageVenueUrl { get; set; }

        public DateTime RealDate { get; set; }

        [Display(Name = "Number Of Participants")]
        public int NumberOfParticipants { get; set; }
    }
}
