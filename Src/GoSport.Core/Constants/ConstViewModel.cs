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
        public const int MaxTownNameLength = 50;
        public const string TownNameMinErrorMessage = "the name of the city should be can not be less 2 characters";
        public const string TownReg = "[a-zA-z\\s]+";
        public const string TownRegErrorMessage = "Town name contains invalid symbols!";

        public const int zipMin = 0;
        public const int zipMax = 10000;
        public const string zipErrorMessage = "Тhe code must be between 0 and 10000";


        public const int MinSportNameLength = 2;
        public const string MinSportErrorMessage = "The Name must be at least 2 characters long!";

        public const int minDisciplineNameLength = 2;
        public const string MinDisciplineNameLengthErrorMessage = "The Name must be at least 2 characters long!";

        public const int minDisciplineDescriptionLength = 20;
        public const string MinDisciplineDescriptionLengthErrorMessage = "The Name must be at least 20 characters long!";


        public const int minDisciplineDiscriptionLength = 20;
        public const string minDisciplineDiscriptionLengthErrMess = "The Description must be at least 20 characters long!";

        public const int minNameDiscriptionLength = 2;
        public const string minNameDiscriptionLengthErrorMessage = "The Name must be at least 2 characters long!";

        public const string DisciplineDoesNotExist = "Discipline Does Not Exist";

        public const string DisciplineWasNotUpdated = "Discipline Was Not Updated";

        public const string DisciplineWasUpdated = "Discipline Was Updated";

        public const int minMessagesFullNameLength = 3;
        public const string mindMessagesFullNameErrorMessage = "The full name must be at least 3 characters long.";

        public const int minSubjectLength = 3;
        public const string minSubjectLengthErrorMessage = "The subject name must be at least 3 characters long.";

        public const int minContentLength = 3;
        public const string minContentLengthErrorMessage = "The content name must be at least 3 characters long.";

        public const int minOrganizerNameLength= 3;
        public const string minOrganizerNameLengthErrorMessage = "The organizer name must be at least 3 characters long.";

        public const int minDescriptionLength = 10;
        public const string minDescriptionLengthErrorMessage = "The description must be at least 10 characters long.";

        public const int minVenueNameLength = 3;
        public const string minVenueNameLengthhErrorMessage = "The venue name must be at least 3 characters long.";

        public const int MinVenueCapacity = 2;
        public const int MaxVenueCapacity = 50;
        public const string CapacityErrorMessage = "Тhe capacity should be between 2 and 50";

        public const int minEvendNameLength = 2;
        public const string minEvendNameLengthErrorMessage = "The event name must be at least 2 characters long.";

        public const int minNumberOfParticipants = 1;
        public const int maxNumberOfParticipants = 50;
        public const string NumberOfParticipantsErrorMessage = "Тhe number of participants should be between 1 and 50";


        public const int maxNameDiscriptionLength = 30;
        public const string maxNameDiscriptionLengthErrorMessage = "The name must not be more than 30 characters";

        public const int maxDescriptionDiscriptionLength = 100;
        public const string maxDescriptionDiscriptionLengthErrorMessage = "The description must not be more than 100 characters";

        public const int maxMessagesFullNameLength = 30;
        public const string maxMessagesFullNameErrorMessage = "The full name must not be more than 30 characters";

        public const int maxSubjectLength = 30;
        public const string maxSubjectLengthErrorMessage = "The subject must not be more than 30 characters";

        public const int maxContentLength = 100;
        public const string maxContentLengthErrorMessage = "The content must not be more than 30 characters";

        public const int maxEvendNameLength = 30;
        public const string maxEvendNameLengthErrorMessage = "The evend name must not be more than 30 characters";

        public const int maxOrganizerNameLength = 30;
        public const string maxOrganizerNameLengthErrorMessage = "The organizer must not be more than 30 characters";

        public const int maxDescriptionLength = 100;
        public const string maxDescriptionLengthErrorMessage = "The description must not be more than 100 characters";

        public const int MaxSportNameLength = 50;
        public const string MaxSportErrorMessage = "The sport must not be more than 50 characters";

        public const int MinDescriptionLength = 20;
        public const string MinDescriptionErrorMessage = "The description must be at least 20 characters long!";

        public const int MaxDescriptionLength = 800;
        public const string MaxDescriptionErrorMessage = "The description must not be more than 800 characters";

        public const string TownNameMaxErrorMessage = "The town name must not be more than 50 characters";

        public const int maxVenueNameLength = 50;
        public const string maxVenueNameLengthhErrorMessage = "The venue name must not be more than 50 characters";

        public const int minAddressLength = 5;
        public const string minAddressErrorMessage = "The address must be at least 5 characters long!";
        public const int maxAddressLength = 50;
        public const string maxAddressErrorMessage = "The address name must not be more than 50 characters";
    }
}
