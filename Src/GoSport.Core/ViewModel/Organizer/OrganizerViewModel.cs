using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Organizer
{
    public class OrganizerViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ConstViewModel.minOrganizerNameLength, ErrorMessage = ConstViewModel.minOrganizerNameLengthErrorMessage)]
        [MaxLength(ConstViewModel.maxOrganizerNameLength, ErrorMessage = ConstViewModel.maxOrganizerNameLengthErrorMessage)]
        public string Name { get; set; }

        [Required]
        [MinLength(ConstViewModel.minDescriptionLength, ErrorMessage = ConstViewModel.minDescriptionLengthErrorMessage)]
        [MaxLength(ConstViewModel.maxDescriptionLength, ErrorMessage = ConstViewModel.maxDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        public string President { get; set; }
    }
}
