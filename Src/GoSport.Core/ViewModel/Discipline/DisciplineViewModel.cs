using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Discipline
{
    public class DisciplineViewModel 
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ConstViewModel.minDisciplineNameLength, ErrorMessage = ConstViewModel.MinDisciplineNameLengthErrorMessage)]
        [MaxLength(ConstViewModel.maxNameDiscriptionLength, ErrorMessage = ConstViewModel.maxNameDiscriptionLengthErrorMessage)]
        public string Name { get; set; }


        [Required]
        [MaxLength(ConstViewModel.maxDescriptionDiscriptionLength, ErrorMessage = ConstViewModel.maxDescriptionDiscriptionLengthErrorMessage)]
        [MinLength(ConstViewModel.minDisciplineDescriptionLength, ErrorMessage = ConstViewModel.MinDisciplineDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Sport")]
        public int SportId { get; set; }
        public string SportName { get; set; }

    }
}
