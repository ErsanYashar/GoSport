using GoSport.Core.ViewModel.Town;
using GoSport.Infrastructure.Data.DateModels;

namespace GoSport.Core.Services.Interfaces
{
    public interface ITown
    {
        IEnumerable<SelectTownViewModel> GetAllTownNames();
        IEnumerable<TownViewModel> GetAllTowns();
        Town AddTown(TownViewModel model);
    }
}
