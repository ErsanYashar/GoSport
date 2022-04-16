using GoSport.Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Core.ViewModel.Organizer
{
    public class AddOrganizerViewModel
    {

        [Required]
        [MinLength(ConstViewModel.minOrganizerNameLength, ErrorMessage = ConstViewModel.minOrganizerNameLengthErrorMessage)]
        [MaxLength(ConstViewModel.maxOrganizerNameLength, ErrorMessage = ConstViewModel.maxOrganizerNameLengthErrorMessage)]
        public string Name { get; set; }

        [Required]
        [MinLength(ConstViewModel.minDescriptionLength, ErrorMessage = ConstViewModel.minDescriptionLengthErrorMessage)]
        [MaxLength(ConstViewModel.maxDescriptionLength, ErrorMessage = ConstViewModel.maxDescriptionLengthErrorMessage)]
        public string Description { get; set; }
    }
}
