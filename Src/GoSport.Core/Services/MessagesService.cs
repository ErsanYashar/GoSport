using AutoMapper;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Message;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.AspNetCore.Identity;

namespace GoSport.Core.Services
{
    public class MessagesService : BaseService, IMessagesService
    {
        public MessagesService(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context) 
            : base(mapper, userManager, context)
        {
        }

        public Message Send(MessageViewModel model, User user)
        {
            var message = this.Mapper.Map<Message>(model);
            if (user != null)
            {
                message.UserId = user.Id;
            }

            this.Context.Messages.Add(message);
            this.Context.SaveChanges();

            return message;
        }
    }
}
