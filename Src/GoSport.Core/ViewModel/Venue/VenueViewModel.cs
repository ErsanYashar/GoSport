using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Venue
{
    public class VenueViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ConstViewModel.minVenueNameLength, ErrorMessage = ConstViewModel.minVenueNameLengthhErrorMessage)]
        public string Name { get; set; }
        public string Address { get; set; }

        [Required]
        [Range(ConstViewModel.MinVenueCapacity, ConstViewModel.MaxVenueCapacity, ErrorMessage = ConstViewModel.CapacityErrorMessage)]
        public int Capacity { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "Image Url")]
        public string ImageVenueUrl { get; set; }

        [Required]
        public int TownId { get; set; }

        public string Town { get; set; }

    }
}
