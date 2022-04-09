using System.ComponentModel.DataAnnotations;
using static GoSport.Infrastructure.Data.DataConst.DataConstants;

namespace GoSport.Infrastructure.Data.DateModels
{
    public class Town
    {
        public Town()
        {
            this.Venues = new List<Venue>();
            this.Users = new List<User>();  
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TownNameMaxLength)]
        public string Name { get; set; }

        public int zipCode { get; set; }

        public virtual ICollection<User> Users { get; set; } 
        public virtual ICollection<Venue> Venues { get; set; } 
    }
}
