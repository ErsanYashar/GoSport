using GoSport.Core.ViewModel.Town;

namespace GoSport.Core.Services.Interfaces
{
    public interface ITown
    {
        IEnumerable<SelectTownViewModel> GetAllTownNames();
    }
}
