using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GoSport.Infrastructure.Data.DataConst.DataConstants;
namespace GoSport.Infrastructure.Data.DateModels
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(VenueNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(VenueAddressMaxLength)]
        public string Address { get; set; }

        [Required]
        public int Capacity { get; set; }

        public string ImageVenueUrl { get; set; }

        public int TownId { get; set; }

        [ForeignKey(nameof(TownId))]
        public virtual Town Town { get; set; }

        public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();
    }
}
