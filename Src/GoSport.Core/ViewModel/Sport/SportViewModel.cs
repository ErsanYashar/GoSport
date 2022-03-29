using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Sport
{
    public class SportViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ConstViewModel.MinSportNameLength, ErrorMessage = ConstViewModel.MinSportErrorMessage)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Image Url")]
        public string ImageSportUrl { get; set; }

    }
}
