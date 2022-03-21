using AutoMapper;
using GoSport.Core.ViewModel.User;
using GoSport.Infrastructure.Data.DateModels;

namespace GoSport.Core.AutoMapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            this.CreateMap<User, RegisterViewModel>().ReverseMap();
        }
    }
}
