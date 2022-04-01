using GoSport.Core.ViewModel.Message;
using GoSport.Infrastructure.Data.DateModels;

namespace GoSport.Core.Services.Interfaces
{
    public interface IMessagesService
    {
        Message Send(MessageViewModel model, User user);
    }
}
