using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Discipline
{
    public class AddDisciplineViewModel 
    {
        [Required]
        [MinLength(ConstViewModel.minNameDiscriptionLength, ErrorMessage = ConstViewModel.minNameDiscriptionLengthErrorMessage)]
        [MaxLength(ConstViewModel.maxNameDiscriptionLength, ErrorMessage = ConstViewModel.maxNameDiscriptionLengthErrorMessage)]
        public string Name { get; set; }

        [Required]
        [MinLength(ConstViewModel.minDisciplineDescriptionLength, ErrorMessage = ConstViewModel.minDisciplineDiscriptionLengthErrMess)]
        [MaxLength(ConstViewModel.maxDescriptionDiscriptionLength, ErrorMessage = ConstViewModel.maxDescriptionDiscriptionLengthErrorMessage)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Sport")]
        public int SportId { get; set; }


    }
}
