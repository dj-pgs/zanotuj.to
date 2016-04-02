using System.ComponentModel.DataAnnotations;

namespace Zanotuj.To.WebApplication.Repository
{
    public class HashTag : BusinessModel
    {
        [Key]
        public string Name { get; set; }

        public int NoteId { get; set; }

        public Note Note { get; set; }
    }
}