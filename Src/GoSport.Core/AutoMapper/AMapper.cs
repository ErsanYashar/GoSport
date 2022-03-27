using GoSport.Core.ViewModel.User;
using GoSport.Infrastructure.Data.DateModels;
using AutoMapper;


namespace GoSport.Core.AutoMapper
{
    public class AMapper : Profile
    {
        public AMapper()
        {
            this.CreateMap<User, RegisterViewModel>().ReverseMap();

            this.CreateMap<UpdateAccountViewModel, User>()
                .ForMember(u => u.UserName, pvm => pvm.MapFrom(x => x.Username))
                .ReverseMap();

            this.CreateMap<User, UsersViewModel>()
              .ForMember(uv => uv.Id, u => u.MapFrom(x => x.Id))
              .ForMember(uv => uv.Username, u => u.MapFrom(x => x.UserName))
              .ReverseMap();
        }
    }
}
