using System.ComponentModel.DataAnnotations;
using static GoSport.Infrastructure.Data.DataConst.DataConstants;

namespace GoSport.Infrastructure.Data.DateModels
{
    public class Sport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(SportNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(SportDescriptionMaxLength)]
        public string Description { get; set; }

        public string ImageSportUrl { get; set; }

        public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();

    }
}
