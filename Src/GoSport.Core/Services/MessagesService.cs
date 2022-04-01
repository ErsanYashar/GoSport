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

        public IEnumerable<AllMessageViewModel> GetAllMessages()
        {
            var messages = this.Context
                .Messages
                .OrderBy(m => m.PublishedOn)
                .Select(x => new AllMessageViewModel
                {
                    Username= x.User.UserName,
                    FullName = x.FullName,
                    Email = x.Email,
                    Subject = x.Subject,
                    Content = x.Content,
                    PublishedOn = x.PublishedOn
                })
                .ToList();

           // var mesViewModel = this.Mapper.Map<IList<Message>, IEnumerable<AllMessageViewModel>>(messages);
            return messages;
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
