using AutoMapper;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Sport;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;

namespace GoSport.Core.Services
{
    public class SportsService : BaseService , ISportsService
    {
        public SportsService(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context) 
            : base(mapper, userManager, context)
        {
        }

        public IEnumerable<SportViewModel> GetAllSports()
        {
            var sports = this.Context
            .Sports
            .OrderBy(s => s.Name)
            .ToList();

            var sportModel = this.Mapper.Map<IList<Sport>, IEnumerable<SportViewModel>>(sports);

            return sportModel;

        }

        public Sport Add(SportViewModel model)
        {
            var sport = this.Mapper.Map<Sport>(model);

            this.Context.Sports.Add(sport);
            this.Context.SaveChanges();

            return sport;
        }

        public SportViewModel GetSportById(int id)
        {
            var sport = this.Context
                .Sports
                .FirstOrDefault(s => s.Id == id);

            var sportModel = this.Mapper.Map<SportViewModel>(sport);

            return sportModel;
        }

        public SportViewModel UpdateSport(SportViewModel model)
        {
            var sport = this.Context
                .Sports
                .FirstOrDefault(s => s.Id == model.Id);

            if (sport == null)
            {
                return null;
            }

            sport.Name = model.Name;
            sport.Description = model.Description;
            sport.ImageSportUrl = model.ImageSportUrl;
            this.Context.SaveChanges();

            var sportModel = this.Mapper.Map<SportViewModel>(sport);

            return sportModel;
        }
    }
}
