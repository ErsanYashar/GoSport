using AutoMapper;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Town;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;

namespace GoSport.Core.Services
{
    public class TownService : BaseService, ITown
    {

        public TownService(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context) 
            : base(mapper, userManager, context)
        {
        }

        public IEnumerable<SelectTownViewModel> GetAllTownNames()
        {
            var town = Context.Towns
                .Select(x => new SelectTownViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .OrderBy(x => x.Name)
                .ToList();
                
            return town;
        }

        public IEnumerable<TownViewModel> GetAllTowns()
        {
            var towns = this.Context
                .Towns
                .OrderBy(t => t.Name)
                .AsQueryable();

            var townViewModels = this.Mapper.Map<IQueryable<Town>, IEnumerable<TownViewModel>>(towns);

            return townViewModels;
        }

        public Town AddTown(TownViewModel model)
        {
            var town = this.Mapper.Map<Town>(model);

            this.Context.Towns.Add(town);
            this.Context.SaveChanges();

            return town;
        }

        public TownViewModel GetTownById(int id)
        {
            var town = this.Context
                .Towns
                .FirstOrDefault(t => t.Id == id);

            var townViewModel = this.Mapper.Map<TownViewModel>(town);

            return townViewModel;
        }

        public TownViewModel UpdateTown(TownViewModel model)
        {
            var town = this.Context
                .Towns
                .FirstOrDefault(t => t.Id == model.Id);

            if (town == null)
            {
                return null;
            }

            town.Name = model.Name;
            town.zipCode = model.zipCode;

            this.Context.SaveChanges();
            var townViewModel = this.Mapper.Map<TownViewModel>(town);
            return townViewModel;
        }
    }
}
