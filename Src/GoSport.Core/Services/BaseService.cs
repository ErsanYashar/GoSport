using AutoMapper;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;

namespace GoSport.Core.Services
{
    public abstract class BaseService
    {

        public BaseService(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context)
        {
            this.Context = context;
            this.Mapper = mapper;
            this.UserManager = userManager;
        }
        public ApplicationDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public UserManager<User> UserManager { get; set; }

    }
}
