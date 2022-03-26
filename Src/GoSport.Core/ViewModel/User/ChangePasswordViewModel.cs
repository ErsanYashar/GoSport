using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.User
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(ConstViewModel.MinPasswordLength, ErrorMessage = ConstViewModel.MinPasswordLengthErrorMessage)]
        [MaxLength(ConstViewModel.MaxPasswordLength, ErrorMessage = ConstViewModel.MinPasswordLengthErrorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Compare(ConstViewModel.ChangePassword, ErrorMessage = ConstViewModel.ConfirmPasswordError)]
        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }
    }
}
