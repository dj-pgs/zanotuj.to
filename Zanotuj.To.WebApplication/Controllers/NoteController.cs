using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
           var result = _noteService.CreateNote(model);
            return View(new NoteAddViewModel());
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NoteEditViewmodel note)
        {
            return View();
        }

        public ActionResult View(int id)
        {
            NoteViewViewModel note = _noteService.GetNoteForView(id);
            return View(note);
        }

    }

    public class NoteViewViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public string Author { get; set; }
        public IEnumerable<string> HashTags { get; set; }
        public DateTime CreateTime { get; set; }
        public int DaysAgo { get; set; }
    }

    public class NoteEditViewmodel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string HashTags { get; set; }

    }
}