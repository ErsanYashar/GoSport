using GoSport.Core.ViewModel.Sport;

namespace GoSport.Core.Services.Interfaces
{
    public interface ISportsService
    {
        IEnumerable<SportViewModel> GetAllSports();
    }
}
