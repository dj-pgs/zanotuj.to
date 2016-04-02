using System.ComponentModel.DataAnnotations.Schema;
using Zanotuj.To.WebApplication.Models;

namespace Zanotuj.To.WebApplication.Repository
{
    public class Note :BusinessModel
    {
        public string NoteContent { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}