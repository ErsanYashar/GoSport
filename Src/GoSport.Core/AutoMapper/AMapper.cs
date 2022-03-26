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
        }
    }
}
