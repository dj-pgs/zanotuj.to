using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zanotuj.To.WebApplication.Services;

namespace Zanotuj.To.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly INoteService _noteService;

        public HomeController(INoteService noteService)
        {
            _noteService = noteService;
        }

        public ActionResult Index(string query=null)
        {
            List<NoteViewModel> notes;
            if (query == null)
            {
                notes = _noteService.GetNotes();
            }
            else
            {
                notes = _noteService.GetNotes(query);
                ViewBag.SearchQuery = query;
            }
            return View(notes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Note()
        {
            return View();
        }
    }
}