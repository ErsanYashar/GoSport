using GoSport.Core.ViewModel.User;
using GoSport.Infrastructure.Data.DateModels;
using AutoMapper;
using GoSport.Core.ViewModel.Town;
using GoSport.Core.ViewModel.Sport;

namespace GoSport.Core.AutoMapper
{
    public class AMapper : Profile
    {
        public AMapper()
        {

            //user

            this.CreateMap<User, RegisterViewModel>().ReverseMap();

            this.CreateMap<UpdateAccountViewModel, User>()
                .ForMember(u => u.UserName, pvm => pvm.MapFrom(x => x.Username))
                .ReverseMap();

            this.CreateMap<User, UsersViewModel>()
              .ForMember(uv => uv.Id, u => u.MapFrom(x => x.Id))
              .ForMember(uv => uv.Username, u => u.MapFrom(x => x.UserName))
              .ReverseMap();

            //town

            this.CreateMap<Town, TownViewModel>().ReverseMap();


            // Sport

            this.CreateMap<Sport, SportViewModel>().ReverseMap();
        }
    }
}
