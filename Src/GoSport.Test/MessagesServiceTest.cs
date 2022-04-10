using GoSport.Core.Services;
using GoSport.Core.ViewModel.Message;
using GoSport.Infrastructure.Data;
using GoSport.Infrastructure.Data.DateModels;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace GoSport.Test
{
    public class MessagesServiceTest : BaseServiceTests
    {
        [Fact]
        public void SendMessageShouldReturnCorrectMessage()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new MessagesService(this.Mapper, null, context);

            context.Users.Add (new User{ UserName = "Ersan", FirstName = "Ersann", LastName = "Yashar" });
            context.SaveChanges();

            var message = service.Send(new MessageViewModel
            {
                Email = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                FullName = "Ersan",
                Subject = "Subject",
                Content = "Text"
            },
            context.Users.FirstOrDefault());

            var expectedMessage = new Message
            {
                Email = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                FullName = "Ersan",
                Subject = "Subject",
                Content = "Text",
            };

            Assert.True(message.FullName.Equals(expectedMessage.FullName));
            Assert.True(message.Email.Equals(expectedMessage.Email));
            Assert.True(message.Subject.Equals(expectedMessage.Subject));
            Assert.True(message.Content.Equals(expectedMessage.Content));

        }

        [Fact]
        public void SendMessageShouldReturnNoCorrectMessage()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new MessagesService(this.Mapper, null, context);

            context.Users.Add(new User { UserName = "Ersan", FirstName = "Ersann", LastName = "Yashar" });
            context.SaveChanges();

            var message = service.Send(new MessageViewModel
            {
                Email = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                FullName = "Ersan",
                Subject = "Subject",
                Content = "Text"
            },
            context.Users.FirstOrDefault());

            var expectedMessage = new Message
            {
                Email = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                FullName = "Figyan",
                Subject = "Subject",
                Content = "Text",
            };

            Assert.False(message.FullName.Equals(expectedMessage.FullName));

        }

        [Fact]
        public void GetAllMessagesShouldReturnCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new MessagesService(this.Mapper, null, context);

            context.Add(new Message 
            {
                User = new User { UserName = "Ersan", FirstName = "Ersann", LastName = "Yashar" },
                Email = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                FullName = "Ersan",
                Subject = "Subject",
                Content = "Text"
            });
            context.SaveChanges();

            var result = service.GetAllMessages().Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetAllMessagesShouldReturnNoCorrectCount()
        {
            var context = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = new MessagesService(this.Mapper, null, context);

            context.Add(new Message
            {
                User = new User { UserName = "Ersan", FirstName = "Ersann", LastName = "Yashar" },
                Email = "https://www.maxfitness.eu/images/gallery/_MG_0985.JPG",
                FullName = "Ersan",
                Subject = "Subject",
                Content = "Text"
            });
            context.SaveChanges();

            var result = service.GetAllMessages().Count();

            Assert.False(0 == result);
        }
    }
}
