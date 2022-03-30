using AutoMapper;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Discipline;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;

namespace GoSport.Core.Services
{
    public class DisciplinesService : BaseService, IDisciplinesService
    {
        public DisciplinesService(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context) 
            : base(mapper, userManager, context)
        {
        }

        public IEnumerable<DisciplineViewModel> GetAllDisciplines()
        {
            var disciplines = this.Context
                .Disciplines
                .OrderBy(s => s.Name)
                .ToList();

            var disciplinesViewModel = this.Mapper.Map<IList<Discipline>, IEnumerable<DisciplineViewModel>>(disciplines);

            return disciplinesViewModel;
        }

        public IEnumerable<DisciplineViewModel> GetDisciplinesBySportId(int id)
        {
            var disciplines = this.Context
                .Disciplines
                .Where(d => d.SportId == id)
                .OrderBy(d => d.Name)
                .ToList();

            var disciplineModels = this.Mapper.Map<IList<Discipline>, IEnumerable<DisciplineViewModel>>(disciplines);

            return disciplineModels;
        }
    }
}
