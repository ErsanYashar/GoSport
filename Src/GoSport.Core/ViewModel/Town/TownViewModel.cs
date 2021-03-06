using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Town
{
    public class TownViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ConstViewModel.MinTownNameLength, ErrorMessage = ConstViewModel.TownNameMinErrorMessage)]
        [MaxLength(ConstViewModel.MaxTownNameLength, ErrorMessage = ConstViewModel.TownNameMaxErrorMessage)]
        [RegularExpression(ConstViewModel.TownReg, ErrorMessage = ConstViewModel.TownRegErrorMessage)]
        public string Name { get; set; }

        [Range(ConstViewModel.zipMin, ConstViewModel.zipMax, ErrorMessage = ConstViewModel.zipErrorMessage)]
        public int zipCode { get; set; }

    }
}
