using System.ComponentModel.DataAnnotations;

namespace GoSport.Core.ViewModel.Event
{
    public class SearchTownEventViewModel
    {
        [Required]
        public int TownId { get; set; }
    }
}
