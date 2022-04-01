using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Message
{
    public class MessageViewModel
    {
        public string Username { get; set; }

        [Required]
        [MinLength(ConstViewModel.minMessagesFullNameLength, ErrorMessage = ConstViewModel.mindMessagesFullNameErrorMessage)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(ConstViewModel.minSubjectLength, ErrorMessage = ConstViewModel.minSubjectLengthErrorMessage)]
        public string Subject { get; set; }

        [Required]
        [MinLength(ConstViewModel.minContentLength, ErrorMessage = ConstViewModel.minContentLengthErrorMessage)]
        public string Content { get; set; }
    }
}
