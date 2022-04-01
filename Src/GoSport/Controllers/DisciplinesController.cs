using GoSport.Core.Constants;
using GoSport.Core.Services.Interfaces;
using GoSport.Core.ViewModel.Discipline;
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


        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            this.ViewData["Sports"] = this.sportsService.GetAllSports();
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(AddDisciplineViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["Sports"] = this.sportsService.GetAllSports();
                return this.View(model);
            }

            this.disciplinesService.AddDiscipline(model);

            return this.RedirectToAction("All", "Disciplines", new { area = "" });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            this.ViewData["Sports"] = this.sportsService.GetAllSports();
            var discipline = this.disciplinesService.GetDisciplineById(id);

            if (discipline == null)
            {
                this.TempData["Message"] = ConstViewModel.DisciplineDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { area = "" });
            }

            return this.View(discipline);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(DisciplineViewModel model)
        {
            this.ViewData["Sports"] = this.sportsService.GetAllSports();

            var discipline = this.disciplinesService.UpdateDiscipline(model);
            if (discipline == null)
            {
                this.ViewData["Error"] = ConstViewModel.DisciplineWasNotUpdated;
                return this.View(model);
            }

            this.ViewData["Message"] = ConstViewModel.DisciplineWasUpdated;

            return this.View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var discipline = this.disciplinesService.GetDisciplineById(id);
            if (discipline == null)
            {
                this.TempData["Message"] = ConstCore.DisciplineDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { area = ""});
            }

            return this.View(discipline);
        }
    }
}
