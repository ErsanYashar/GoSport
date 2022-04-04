using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GoSport.Infrastructure.Data.DataConst.DataConstants;

namespace GoSport.Infrastructure.Data.DateModels
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventNameLength)]
        public string EventName { get; set; }

        public DateTime Date { get; set; }

        public int OrganizerId { get; set; }

        [ForeignKey(nameof(OrganizerId))]
        public virtual Organizer Organizer { get; set; }

        public int DisciplineId { get; set; }

        [ForeignKey(nameof(DisciplineId))]
        public virtual Discipline Discipline { get; set; }

        public int VenueId { get; set; }

        [ForeignKey(nameof(VenueId))]
        public virtual Venue Venue { get; set; }

        [Required]
        public int NumberOfParticipants { get; set; }
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
