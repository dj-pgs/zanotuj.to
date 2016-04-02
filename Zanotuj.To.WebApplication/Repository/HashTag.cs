using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zanotuj.To.WebApplication.Repository
{
    public class HashTag : BusinessModel
    {
        [Key]
        public string Name { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}