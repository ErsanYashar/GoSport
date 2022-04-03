using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Venue
{
    public class SearchTownViewModel
    {
        [Required]
        public int TownId { get; set; }
    }
}
