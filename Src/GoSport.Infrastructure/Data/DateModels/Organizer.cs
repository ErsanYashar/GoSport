using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GoSport.Infrastructure.Data.DataConst.DataConstants;

namespace GoSport.Infrastructure.Data.DateModels
{
    public class Organizer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(OrganizerNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(OrganizerDescriptionMaxLength)]
        public string Description { get; set; }

        public string PresidentId { get; set; }

        [ForeignKey(nameof(PresidentId))]
        public virtual User President { get; set; }

        public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();
    }
}
