using System.Collections.Generic;
using System.Linq;
using Zanotuj.To.WebApplication.Repository;

namespace Zanotuj.To.WebApplication.Services
{
    public class NoteService : INoteService
    {
        private readonly IApplicationDbContext _context;

        public NoteService(IApplicationDbContext context)
        {
            _context = context;
        }

        public List<NoteViewModel> GetNotes()
        {
            return _context.Notes.Select(note => new NoteViewModel() { Autor = note.User.UserName }).ToList();
        }

    }

    public interface INoteService
    {
        List<NoteViewModel> GetNotes();
    }

    public class NoteViewModel
    {
        public string Autor { get; set; }
    }
}