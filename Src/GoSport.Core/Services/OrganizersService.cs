using AutoMapper;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Organizer;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;

namespace GoSport.Core.Services
{
    public class OrganizersService : BaseService, IOrganizersService
    {
        public OrganizersService(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context) 
            : base(mapper, userManager, context)
        {
        }

        public IEnumerable<OrganizerViewModel> AllOrganizers()
        {
            var organizer = this.Context
               .Organizers
               .Select(x => new OrganizerViewModel
               { 
                   Id = x.Id,
                   Name = x.Name,
                   Description = x.Description
               })
               .OrderBy(x => x.Name)
               .ToList();

            return organizer;
        }

        public Organizer Add(AddOrganizerViewModel model, string username)
        {
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            var organizer = this.Mapper.Map<Organizer>(model);
            organizer.President = user;

            this.Context.Organizers.Add(organizer);
            this.Context.SaveChanges();

            return organizer;
        }

        public OrganizerViewModel organizerById(int id)
        {
            var organizer = this.Context
               .Organizers
               .FirstOrDefault(x => x.Id == id);

            var organizerViewModel = this.Mapper.Map<OrganizerViewModel>(organizer);

            return organizerViewModel;
        }

        public void DeleteOrganization(OrganizerViewModel model)
        {
            var organizer = this.Context
                .Organizers
                .FirstOrDefault(d => d.Id == model.Id);

            if (organizer != null)
            {
                this.Context.Organizers.Remove(organizer);
                this.Context.SaveChanges();
            }
        }
    }
}
