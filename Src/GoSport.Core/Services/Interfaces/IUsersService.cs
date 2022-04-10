using GoSport.Core.ViewModel.User;

namespace GoSport.Core.Services.Interfaces
{
    public interface IUsersService
    {
        IEnumerable<UsersViewModel> GetAllUsers();

    }
}
