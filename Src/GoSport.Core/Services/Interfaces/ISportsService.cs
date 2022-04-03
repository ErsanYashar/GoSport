using GoSport.Core.ViewModel.Sport;
using GoSport.Infrastructure.Data.DateModels;

namespace GoSport.Core.Services.Interfaces
{
    public interface ISportsService
    {
        IList<SportViewModel> GetAllSports();
        Sport Add(AddSportViewModel model);
        SportViewModel GetSportById(int id);
        SportViewModel UpdateSport(SportViewModel model);
    }
}
