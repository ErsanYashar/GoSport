using GoSport.Core.ViewModel.User;
using GoSport.Infrastructure.Data.DateModels;
using AutoMapper;
using GoSport.Core.ViewModel.Town;
using GoSport.Core.ViewModel.Sport;
using GoSport.Core.ViewModel.Discipline;
using GoSport.Core.ViewModel.Message;
using GoSport.Core.ViewModel.Organizer;
using GoSport.Core.ViewModel.Venue;
using GoSport.Core.ViewModel.Event;
using System.Globalization;

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
            this.CreateMap<Sport, AddSportViewModel>().ReverseMap();


            //Discipline

            this.CreateMap<Discipline, DisciplineViewModel>();
             //.ForMember(dvm => dvm.Sport, d => d.MapFrom(x => x.Sport.Name)).ReverseMap();

            this.CreateMap<Discipline, AddDisciplineViewModel>().ReverseMap();

            //message

            this.CreateMap<Message, MessageViewModel>().ReverseMap();

            //this.CreateMap<Message, AllMessageViewModel>()
            //   .ForMember(m => m.Username, m => m.MapFrom(x => x.User.UserName))
            //   .ForMember(m => m.PublishedOn, m => m.MapFrom(x => x.PublishedOn.ToString("dd-MM-yyyy HH:mm:ss")))
            //   .ReverseMap();

            // Organizer

            this.CreateMap<Organizer, AddOrganizerViewModel>().ReverseMap();
            this.CreateMap<Organizer, OrganizerViewModel>().ReverseMap();

            // Venues
            this.CreateMap<Venue, VenueViewModel>().ReverseMap();
            this.CreateMap<Venue, AddVenueViewModel>().ReverseMap();

            //Event
            this.CreateMap<Event, EventViewModel>();
            this.CreateMap<Event, EventViewModel>().ReverseMap();
            this.CreateMap<Event, UpdateEventViewModel>();
            this.CreateMap<Event, CreateEventViewModel>()
           .ReverseMap();



        }
    }
}
