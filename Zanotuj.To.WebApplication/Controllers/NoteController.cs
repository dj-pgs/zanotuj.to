using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Html.Helpers;
using Zanotuj.To.WebApplication.Models;
using Zanotuj.To.WebApplication.Services;

namespace Zanotuj.To.WebApplication.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // GET: Note
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "home");
        }

        public ActionResult Add()
        {
            return View(new NoteAddViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(NoteAddViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            model.Content = HtmlSanitizer.sanitize(model.Content);
            var result = _noteService.GetNotes(model);
            return RedirectToAction("View", new { id = result.Data });
        }

        public ActionResult Edit(int id)
        {
            var model = _noteService.GetNoteForEditViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NoteEditViewModel note, int id)
        {
            note.Content = HtmlSanitizer.sanitize(note.Content);
            if (!ModelState.IsValid)
            {
                return View(note);
            }
            var model = _noteService.EditNote(note, id);
            if (model.IsSuccess)
            {
                return RedirectToAction("View", new {id = id});
            }
            return View(note);
        }
        [AllowAnonymous]
        public ActionResult View(int id)
        {
            NoteViewViewModel note = _noteService.GetNoteForView(id);
            return View(note);
        }

    }

    public class NoteViewViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public string Author { get; set; }
        public IEnumerable<string> HashTags { get; set; }
        public DateTime CreateTime { get; set; }
        public int DaysAgo { get; set; }
    }
}