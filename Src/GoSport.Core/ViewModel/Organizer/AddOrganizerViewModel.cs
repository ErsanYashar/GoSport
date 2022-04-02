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
        public string Name { get; set; }

        [Required]
        [MinLength(ConstViewModel.minDescriptionLength, ErrorMessage = ConstViewModel.minDescriptionLengthErrorMessage)]
        public string Description { get; set; }
    }
}
