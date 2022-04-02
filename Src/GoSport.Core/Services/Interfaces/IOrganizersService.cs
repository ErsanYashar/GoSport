using GoSport.Core.ViewModel.Organizer;
using GoSport.Infrastructure.Data.DateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Core.Services.Interfaces
{
    public interface IOrganizersService
    {
        IEnumerable<OrganizerViewModel> AllOrganizers();

        Organizer Add(AddOrganizerViewModel model, string username);

        OrganizerViewModel organizerById(int id);

        void DeleteOrganization(OrganizerViewModel model);
    }
}
