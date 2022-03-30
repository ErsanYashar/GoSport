using GoSport.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GoSport.Controllers
{
    public class DisciplinesController : Controller
    {
        private readonly IDisciplinesService disciplinesService;
        private readonly ISportsService sportsService;

        public DisciplinesController(ISportsService sportsService, IDisciplinesService disciplinesService)
        {
            this.sportsService = sportsService;
            this.disciplinesService = disciplinesService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All(int? page)
        {
            var disciplines = this.disciplinesService.GetAllDisciplines();

            var pageNumber = page ?? 1;
            var disciplinesPage = disciplines.ToPagedList(pageNumber, 10);

            return this.View(disciplinesPage);
        }
    }
}
