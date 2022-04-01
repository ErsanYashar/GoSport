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

        public Discipline AddDiscipline(AddDisciplineViewModel model)
        {
            var discipline = this.Mapper.Map<Discipline>(model);

            this.Context.Disciplines.Add(discipline);
            this.Context.SaveChanges();

            return discipline;
        }

        public void DeleteDiscipline(DisciplineViewModel model)
        {
            var discipline = this.Context
               .Disciplines
               .FirstOrDefault(d => d.Id == model.Id);

            if (discipline != null)
            {
                this.Context.Disciplines.Remove(discipline);
                this.Context.SaveChanges();
            }
        }

        public IEnumerable<DisciplineViewModel> GetAllDisciplines()
        {
            var disciplines = this.Context
                .Disciplines
                .OrderBy(s => s.Name)
                .Select(x => new DisciplineViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    SportId = x.SportId,
                    SportName = x.Sport.Name

                })
                .ToList();

           // var disciplinesViewModel = this.Mapper.Map<IList<Discipline>, IEnumerable<DisciplineViewModel>>(disciplines);

            return disciplines;
        }

        public DisciplineViewModel GetDisciplineById(int id)
        {

            var disciplines = this.Context
             .Disciplines
             .Select(x => new DisciplineViewModel
             {
                 Id = x.Id,
                 Name = x.Name,
                 Description = x.Description,
                 SportId = x.SportId,
                 SportName = x.Sport.Name

             })
             .FirstOrDefault(d => d.Id == id);

            return disciplines;
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

        public DisciplineViewModel UpdateDiscipline(DisciplineViewModel model)
        {
            var discipline = this.Context
                .Disciplines
                .FirstOrDefault(s => s.Id == model.Id);

            if (discipline == null)
            {
                return null;
            }

            discipline.Name = model.Name;
            discipline.Description = model.Description;
            discipline.SportId = model.SportId;
            this.Context.SaveChanges();

            var disciplineViewModel = this.Mapper.Map<DisciplineViewModel>(discipline);

            return disciplineViewModel;
        }
    }
}
