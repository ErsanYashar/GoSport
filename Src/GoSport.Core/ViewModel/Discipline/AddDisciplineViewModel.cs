using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Discipline
{
    public class AddDisciplineViewModel 
    {
        [Required]
        [MinLength(ConstViewModel.minNameDiscriptionLength, ErrorMessage = ConstViewModel.minNameDiscriptionLengthErrorMessage)]
        public string Name { get; set; }

        [Required]
        [MinLength(ConstViewModel.minDisciplineDescriptionLength, ErrorMessage = ConstViewModel.minDisciplineDiscriptionLengthErrMess)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Sport")]
        public int SportId { get; set; }

    }
}
