using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Town
{
    public class TownViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ConstViewModel.MinTownNameLength, ErrorMessage = ConstViewModel.TownNameMinErrorMessage)]
        [RegularExpression(ConstViewModel.TownReg, ErrorMessage = ConstViewModel.TownRegErrorMessage)]
        public string Name { get; set; }

        [Range(ConstViewModel.zipMin, ConstViewModel.zipMax, ErrorMessage = ConstViewModel.zipErrorMessage)]
        public string zipCode { get; set; }

    }
}
