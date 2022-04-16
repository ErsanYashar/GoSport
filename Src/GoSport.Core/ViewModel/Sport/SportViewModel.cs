using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Sport
{
    public class SportViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ConstViewModel.MinSportNameLength, ErrorMessage = ConstViewModel.MinSportErrorMessage)]
        [MaxLength(ConstViewModel.MaxSportNameLength, ErrorMessage = ConstViewModel.MaxSportErrorMessage)]
        public string Name { get; set; }

        [Required]
        [MinLength(ConstViewModel.MinDescriptionLength, ErrorMessage = ConstViewModel.MinDescriptionErrorMessage)]
        [MaxLength(ConstViewModel.MaxDescriptionLength, ErrorMessage = ConstViewModel.MaxDescriptionErrorMessage)]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Image Url")]
        public string ImageSportUrl { get; set; }

    }
}
