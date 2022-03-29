using GoSport.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GoSport.Controllers
{
    public class SportsController : Controller
    {
        private readonly ISportsService sportsService;

        public SportsController(ISportsService sportsService)
        {
            this.sportsService = sportsService;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult All(int? page)
        {
            var sports = this.sportsService.GetAllSports();

            var pageNumber = page ?? 1;
            var sportsPage = sports.ToPagedList(pageNumber, 10);

            return this.View(sportsPage);
        }
    }
}
