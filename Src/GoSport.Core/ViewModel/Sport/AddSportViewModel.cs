using GoSport.Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Core.ViewModel.Sport
{
    public class AddSportViewModel
    {
        [Required]
        [MinLength(ConstViewModel.MinSportNameLength, ErrorMessage = ConstViewModel.MinSportErrorMessage)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Image Url")]
        public string ImageSportUrl { get; set; }
    }
}
