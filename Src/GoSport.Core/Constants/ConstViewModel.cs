using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Core.Constants
{
    public static class ConstViewModel
    {
        public const int MinUsernameLength = 2;
        public const int MaxUsernameLength = 30;
        public const string UsernameMinLengthErrorMessage = $"The Username must be at least 2 characters long!";
        public const string UsernameMaxLengthErrorMessage = $"Username should not be longer than 30 characters!";

        public const int MaxPasswordLength = 30;
        public const int MinPasswordLength = 4;
        public const string MinPasswordLengthErrorMessage = "Тhe password must be at least 4 characters long";
        public const string MaxPasswordLengthErrorMessage = "Password must be no longer than 30 characters";

        public const string RegisterAccountCompare = "Password";
        public const string ConfirmPasswordErrorMessage = "The password and confirmation password do not match.";


        public const int MinFirstNameLength = 2;
        public const int MaxFirstNameLength = 30;
        public const string FirstNameMinLengthErrorMessage = $"The FirstName must be at least 2 characters long!";
        public const string FirstNameMaxLengthErrorMessage = $"FirstName should not be longer than 30 characters!";


        public const int MinLastNameLength = 2;
        public const int MaxLastNameLength = 30;
        public const string LastNameMinLengthErrorMessage = $"The LastName must be at least 2 characters long!";
        public const string LastNameMaxLengthErrorMessage = $"LastName should not be longer than 30 characters!";

        public const string ChangePassword = "NewPassword";
        public const string ConfirmPasswordError = "The password and confirmation password do not match.";


        public const int MinTownNameLength = 2;
        public const int MaxTownNameLength = 30;
        public const string TownNameMinErrorMessage = "the name of the city should be can not be less 2 characters";
        public const string TownReg = "[a-zA-z\\s]+";
        public const string TownRegErrorMessage = "Town name contains invalid symbols!";

        public const int zipMin = 0;
        public const int zipMax = 10000;
        public const string zipErrorMessage = "Тhe code must be between 0 and 10000";


        public const int MinSportNameLength = 2;
        public const string MinSportErrorMessage = "The Name must be at least 2 characters long!";



    }
}
