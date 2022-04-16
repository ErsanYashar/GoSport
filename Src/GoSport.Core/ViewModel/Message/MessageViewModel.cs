using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Message
{
    public class MessageViewModel
    {
        public string Username { get; set; }

        [Required]
        [MinLength(ConstViewModel.minMessagesFullNameLength, ErrorMessage = ConstViewModel.mindMessagesFullNameErrorMessage)]
        [MaxLength(ConstViewModel.maxMessagesFullNameLength, ErrorMessage = ConstViewModel.maxMessagesFullNameErrorMessage)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(ConstViewModel.minSubjectLength, ErrorMessage = ConstViewModel.minSubjectLengthErrorMessage)]
        [MaxLength(ConstViewModel.maxSubjectLength, ErrorMessage = ConstViewModel.maxSubjectLengthErrorMessage)]
        public string Subject { get; set; }

        [Required]
        [MinLength(ConstViewModel.minContentLength, ErrorMessage = ConstViewModel.minContentLengthErrorMessage)]
        [MaxLength(ConstViewModel.maxContentLength, ErrorMessage = ConstViewModel.maxContentLengthErrorMessage)]
        public string Content { get; set; }
    }
}
