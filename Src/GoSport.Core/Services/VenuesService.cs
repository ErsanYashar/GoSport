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

        public VenueViewModel VenueById(int id)
        {
            var venue = this.Context
              .Venues
               .Select(x => new VenueViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Capacity = x.Capacity,
                   ImageVenueUrl = x.ImageVenueUrl,
                   Town = x.Town.Name,
                   Address = x.Address,
               })
              .FirstOrDefault(v => v.Id == id);

            return venue;
        }

        public IEnumerable<VenueViewModel> AllVenuesByTownId(int townId)
        {
            var venues = this.Context
                .Venues
                .Where(v => v.TownId == townId)
                 .Select(x => new VenueViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     ImageVenueUrl = x.ImageVenueUrl,
                     Capacity = x.Capacity,
                     Town = x.Town.Name,
                     Address = x.Address,
                 })
                .OrderBy(v => v.Name)
                .ToList();

            return venues;
        }

        public void DeleteVenue(VenueViewModel model)
        {
            var venue = this.Context
               .Venues
               .FirstOrDefault(d => d.Id == model.Id);

            if (venue != null)
            {
                this.Context.Venues.Remove(venue);
                this.Context.SaveChanges();
            }
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

        public VenueViewModel UpdateVenue(VenueViewModel model)
        {
            var venue = this.Context
                .Venues
                .FirstOrDefault(v => v.Id == model.Id);

            if (venue == null)
            {
                return null;
            }

            venue.Name = model.Name;
            venue.Address = model.Address;
            venue.Capacity = model.Capacity;
            venue.ImageVenueUrl = model.ImageVenueUrl;
            venue.TownId = model.TownId;
            this.Context.SaveChanges();

            var venueModel = this.Mapper.Map<VenueViewModel>(venue);

            return venueModel;
        }

    }
}
