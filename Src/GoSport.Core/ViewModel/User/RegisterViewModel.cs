using GoSport.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.User
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(ConstViewModel.MinUsernameLength, ErrorMessage = ConstViewModel.UsernameMinLengthErrorMessage)]
        [MaxLength(ConstViewModel.MaxUsernameLength, ErrorMessage = ConstViewModel.UsernameMaxLengthErrorMessage)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(ConstViewModel.MinPasswordLength, ErrorMessage = ConstViewModel.MinPasswordLengthErrorMessage)]
        [MaxLength(ConstViewModel.MaxPasswordLength, ErrorMessage = ConstViewModel.MaxPasswordLengthErrorMessage)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(ConstViewModel.RegisterAccountCompare, ErrorMessage = ConstViewModel.ConfirmPasswordErrorMessage)]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(ConstViewModel.MinFirstNameLength, ErrorMessage = ConstViewModel.FirstNameMinLengthErrorMessage)]
        [MaxLength(ConstViewModel.MaxFirstNameLength, ErrorMessage = ConstViewModel.FirstNameMaxLengthErrorMessage)]
        public string FirstName { get; set; }


        [Required]
        [MinLength(ConstViewModel.MinLastNameLength, ErrorMessage = ConstViewModel.LastNameMinLengthErrorMessage)]
        [MaxLength(ConstViewModel.MaxLastNameLength, ErrorMessage = ConstViewModel.LastNameMaxLengthErrorMessage)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public int TownId { get; set; }
    }
}
