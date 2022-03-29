using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Discipline
{
    public class DisciplineViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ConstViewModel.minDisciplineNameLength, ErrorMessage = ConstViewModel.MinDisciplineNameLengthErrorMessage)]
        public string Name { get; set; }


        [Required]
        [MinLength(ConstViewModel.minDisciplineDescriptionLength, ErrorMessage = ConstViewModel.MinDisciplineDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Sport")]
        public int SportId { get; set; }

        public string Sport { get; set; }
    }
}
