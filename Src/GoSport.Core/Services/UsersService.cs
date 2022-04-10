using AutoMapper;
using GoSport.Core.ViewModel.User;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Core.Services.Interfaces
{
    public class UsersService : BaseService, IUsersService
    {
        public UsersService(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context) 
            : base(mapper, userManager, context)
        {
        }

        public IEnumerable<UsersViewModel> GetAllUsers()
        {
            var users = this.Context
                .Users
                .OrderBy(x => x.UserName)
                .ToList();

            var usersViewModel = this.Mapper.Map<IList<User>, IEnumerable<UsersViewModel>>(users);

            return usersViewModel;
        }

    }
}
