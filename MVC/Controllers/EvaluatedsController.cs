using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services.Bases;
using BLL.Models;
using BLL.DAL;
using Microsoft.AspNetCore.Authorization;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class EvaluatedsController : MvcController
    {
        // Service injections:
        private readonly IService<Evaluated, EvaluatedModel> _evaluatedService;

        /* Can be uncommented and used for many to many relationships. Entity must be replaced with the related entiy name in the controller and views. */
        //private readonly IService<{Entity}, {Entity}Model> _{Entity}Service;

        public EvaluatedsController(
			IService<Evaluated, EvaluatedModel> evaluatedService

            /* Can be uncommented and used for many to many relationships. Entity must be replaced with the related entiy name in the controller and views. */
            //, IService<{Entity}, {Entity}Model> {Entity}Service
        )
        {
            _evaluatedService = evaluatedService;

            /* Can be uncommented and used for many to many relationships. Entity must be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. Entity must be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Evaluateds
        [Authorize(Roles = "Admin")]

        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _evaluatedService.Query().ToList();
            return View(list);
        }

        // GET: Evaluateds/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _evaluatedService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // GET: Evaluateds/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Evaluateds/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(EvaluatedModel evaluated)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _evaluatedService.Create(evaluated.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = evaluated.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(evaluated);
        }

        // GET: Evaluateds/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _evaluatedService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Evaluateds/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EvaluatedModel evaluated)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _evaluatedService.Update(evaluated.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = evaluated.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(evaluated);
        }

        // GET: Evaluateds/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _evaluatedService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Evaluateds/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _evaluatedService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
