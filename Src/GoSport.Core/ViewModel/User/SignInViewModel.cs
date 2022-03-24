using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.User
{
    public class SignInViewModel
    {
        [Required]
        [MinLength(ConstViewModel.MinUsernameLength, ErrorMessage = ConstViewModel.UsernameMinLengthErrorMessage)]
        [MaxLength(ConstViewModel.MaxUsernameLength, ErrorMessage = ConstViewModel.UsernameMaxLengthErrorMessage)]
        public string Username { get; set; }

        [Required]
        [MinLength(ConstViewModel.MinPasswordLength, ErrorMessage = ConstViewModel.MinPasswordLengthErrorMessage)]
        [MaxLength(ConstViewModel.MaxPasswordLength, ErrorMessage = ConstViewModel.MaxPasswordLengthErrorMessage)]
        public string Password { get; set; }

    }
}
