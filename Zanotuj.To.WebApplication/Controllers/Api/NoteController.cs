using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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

        public List<NoteViewModel> Get()
        {
            return _noteService.GetNotes();
        } 
    }
}
