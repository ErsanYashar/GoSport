using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Town;
using GoSport.Infrastructure.Data;

namespace GoSport.Core.Services
{
    public class TownService : ITown
    {

        private readonly ApplicationDbContext dbContex;

        public TownService(ApplicationDbContext dbContex)
        {
            this.dbContex = dbContex;
        }


        public IEnumerable<SelectTownViewModel> GetAllTownNames()
        {
            var town = dbContex.Towns
                .Select(x => new SelectTownViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .OrderBy(x => x.Name)
                .ToList();
                
            return town;
        }
    }
}
