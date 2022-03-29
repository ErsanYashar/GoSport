using GoSport.Core.ViewModel.Sport;
using GoSport.Infrastructure.Data.DateModels;

namespace GoSport.Core.Services.Interfaces
{
    public interface ISportsService
    {
        IEnumerable<SportViewModel> GetAllSports();
        Sport Add(SportViewModel model);
        SportViewModel GetSportById(int id);
        SportViewModel UpdateSport(SportViewModel model);
    }
}
