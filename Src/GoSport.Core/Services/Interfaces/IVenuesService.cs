using GoSport.Core.ViewModel.Venue;
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
    }
}
