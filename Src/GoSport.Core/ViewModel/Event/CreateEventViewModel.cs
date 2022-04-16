using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Event
{
    public class CreateEventViewModel
    {

        [Required]
        [MinLength(ConstViewModel.minEvendNameLength, ErrorMessage = ConstViewModel.minEvendNameLengthErrorMessage)]
        [MaxLength(ConstViewModel.maxEvendNameLength, ErrorMessage = ConstViewModel.maxEvendNameLengthErrorMessage)]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Required]
        [Display(Name = "Date and Time of Events")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Organizer")]
        public int OrganizerId { get; set; }

        [Required]
        [Display(Name = "Discipline")]
        public int DisciplineId { get; set; }

        [Required]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        [Required]
        [Range(ConstViewModel.minNumberOfParticipants, ConstViewModel.maxNumberOfParticipants, ErrorMessage = ConstViewModel.NumberOfParticipantsErrorMessage)]
        [Display(Name = "Number Of Participants")]
        public int NumberOfParticipants { get; set; }
    }
}
