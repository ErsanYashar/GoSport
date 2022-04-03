using GoSport.Core.ViewModel.Venue;
using GoSport.Infrastructure.Data.DateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Core.Services.Interfaces
{
    public interface IVenuesService 
    {
        IEnumerable<VenueViewModel> GetAllVenues();

        Venue AddVenue(AddVenueViewModel model);

        VenueViewModel VenueById(int id);

        VenueViewModel UpdateVenue(VenueViewModel model);

        void DeleteVenue(VenueViewModel model);
    }
}
