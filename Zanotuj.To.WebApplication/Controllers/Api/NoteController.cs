

using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using Zanotuj.To.WebApplication.Services;

namespace Zanotuj.To.WebApplication.Controllers.Api
{
    public class NoteController : ApiController
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [System.Web.Http.HttpGet]
        public List<NoteViewModel> Get([FromUri]SearchDto searchModel)
        {
            return _noteService.GetNotes(searchModel);
        }

        [ValidateAntiForgeryToken]
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Delete(int id)
        {
            _noteService.Delete(id);
            return Ok();
        }
    }

    public class SearchDto
    {
        public List<string> Hashtags { get; set; }
        public List<string> Authors { get; set; }

        public List<string> Phrase { get; set; }

        public int PageSize { get; set; }

        public int Page { get; set; }

        public SearchDto()
        {
            Hashtags = new List<string>();
            Authors = new List<string>();
            Phrase = new List<string>();
        }
    }
}
