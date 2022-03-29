using GoSport.Core.ViewModel.Discipline;

namespace GoSport.Core.Services.Interfaces
{
    public interface IDisciplinesService
    {
        IEnumerable<DisciplineViewModel> GetDisciplinesBySportId(int id);
    }
}
