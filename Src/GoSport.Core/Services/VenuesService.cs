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

        public Venue AddVenue(AddVenueViewModel model)
        {
            var venue = this.Mapper.Map<Venue>(model);

            this.Context.Venues.Add(venue);
            this.Context.SaveChanges();

            return venue;
        }

        public IEnumerable<VenueViewModel> GetAllVenues()
        {
            var venues = this.Context
           .Venues
           .OrderBy(v => v.Name)
           .Select(x => new VenueViewModel
           { 
               Id= x.Id,
               Name= x.Name,
               ImageVenueUrl= x.ImageVenueUrl,
               Town= x.Town.Name,
               Address = x.Address,              
           })
           .ToList();

           // var venuesView = this.Mapper.Map<IList<Venue>, IEnumerable<VenueViewModel>>(venues);
            return venues;
        }
    }
}
