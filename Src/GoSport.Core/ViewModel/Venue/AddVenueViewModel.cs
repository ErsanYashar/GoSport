using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Venue
{
    public class AddVenueViewModel
    {

        [Required]
        [MinLength(ConstViewModel.minVenueNameLength, ErrorMessage = ConstViewModel.minVenueNameLengthhErrorMessage)]
        [MaxLength(ConstViewModel.maxVenueNameLength, ErrorMessage = ConstViewModel.maxVenueNameLengthhErrorMessage)]
        public string Name { get; set; }

        [Required]
        [MinLength(ConstViewModel.minAddressLength, ErrorMessage = ConstViewModel.minAddressErrorMessage)]
        [MaxLength(ConstViewModel.maxAddressLength, ErrorMessage = ConstViewModel.maxAddressErrorMessage)]
        public string Address { get; set; }

        [Required]
        [Range(ConstViewModel.MinVenueCapacity, ConstViewModel.MaxVenueCapacity, ErrorMessage = ConstViewModel.CapacityErrorMessage)]
        public int Capacity { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "Image Url")]
        public string ImageVenueUrl { get; set; }

        [Required]
        [Display(Name = "Town")]
        public int TownId { get; set; }

    }
}
