using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services.Bases;
using BLL.Models;
using BLL.DAL;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;

// Generated from Custom Template.

namespace MVC.Controllers
{
    [Authorize]
    public class EvaluationsController : MvcController
    {

        // Service injections:
        private readonly IService<Evaluation, EvaluationModel> _evaluationService;
        private readonly IService<User, UserModel> _userService;

        /* Can be uncommented and used for many to many relationships. Entity must be replaced with the related entiy name in the controller and views. */
        private readonly IService<Evaluated, EvaluatedModel> _evaluatedService;

        public EvaluationsController(
			IService<Evaluation, EvaluationModel> evaluationService
            , IService<User, UserModel> userService

            /* Can be uncommented and used for many to many relationships. Entity must be replaced with the related entiy name in the controller and views. */
            , IService<Evaluated, EvaluatedModel> evaluatedService
        )
        {
            _evaluationService = evaluationService;
            _userService = userService;

            /* Can be uncommented and used for many to many relationships. Entity must be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["UserId"] = new SelectList(_userService.Query().ToList(), "Record.Id", "UserName");
            //ViewData["UserId"] = new SelectList(_userService.Query().ToList(), "Record.Id", "Name");

            /* Can be uncommented and used for many to many relationships. Entity must be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
            //ViewBag.EvaluatedIds = new MultiSelectList(_evaluatedService.Query().ToList(), "Record.Id", "NameAndSurname");
        }

        // GET: Evaluations
        [AllowAnonymous] // Herkes TÜm kitapları görebilir üye olmayanlar bile.
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _evaluationService.Query().ToList();
            return View(list);
        }

        // GET: Evaluations/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _evaluationService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // GET: Evaluations/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Evaluations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EvaluationModel evaluation)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _evaluationService.Create(evaluation.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = evaluation.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(evaluation);
        }

        // GET: Evaluations/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _evaluationService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Evaluations/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EvaluationModel evaluation)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _evaluationService.Update(evaluation.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = evaluation.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(evaluation);
        }

        // GET: Evaluations/Delete/5
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _evaluationService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Evaluations/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _evaluationService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
