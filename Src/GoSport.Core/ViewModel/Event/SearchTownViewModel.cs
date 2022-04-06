using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Event
{
    public class SearchTownViewModel
    {
        [Required]
        public int TownId { get; set; }
    }
}
