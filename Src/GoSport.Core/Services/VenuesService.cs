using AutoMapper;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Venue;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;

namespace GoSport.Core.Services
{
    public class VenuesService : BaseService , IVenuesService
    {
        public VenuesService(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context) 
            : base(mapper, userManager, context)
        {
        }

        public IEnumerable<VenueViewModel> GetAllVenues()
        {
            var venues = this.Context
           .Venues
           .OrderBy(v => v.Name)
           .ToList();

            var venuesView = this.Mapper.Map<IList<Venue>, IEnumerable<VenueViewModel>>(venues);
            return venuesView;
        }
    }
}
