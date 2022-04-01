using GoSport.Core.ViewModel.Discipline;
using GoSport.Infrastructure.Data.DateModels;

namespace GoSport.Core.Services.Interfaces
{
    public interface IDisciplinesService
    {
        IEnumerable<DisciplineViewModel> GetDisciplinesBySportId(int id);

        IEnumerable<DisciplineViewModel> GetAllDisciplines();

        Discipline AddDiscipline(AddDisciplineViewModel model);

        DisciplineViewModel GetDisciplineById(int id);

        DisciplineViewModel UpdateDiscipline(DisciplineViewModel model);

        void DeleteDiscipline(DisciplineViewModel model);
    }
}
