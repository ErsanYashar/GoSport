using System.ComponentModel.DataAnnotations;
using static GoSport.Infrastructure.Data.DataConst.DataConstants;

namespace GoSport.Infrastructure.Data.DateModels
{
    public class Discipline
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DisciplineNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }

        public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();

    }
}
