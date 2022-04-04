using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GoSport.Infrastructure.Data.DataConst.DataConstants;

namespace GoSport.Infrastructure.Data.DateModels
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public int TownId { get; set; }

        [ForeignKey(nameof(TownId))]
        public virtual Town Town { get; set; }
        public string? PhotoUrl { get; set; }
        public virtual ICollection<Organizer> Organizers { get; set; } = new HashSet<Organizer>();
        public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();
        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
