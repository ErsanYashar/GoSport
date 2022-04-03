using GoSport.Core.ViewModel.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Core.Services.Interfaces
{
    public interface IEventsService
    {
        IEnumerable<EventViewModel> AllEvents();
    }
}
