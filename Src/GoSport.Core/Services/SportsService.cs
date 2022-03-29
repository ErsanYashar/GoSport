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
    }
}
