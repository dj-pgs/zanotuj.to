

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zanotuj.To.WebApplication.Repository
{
    public class HashTag : BusinessModel
    {
        [Index("IX_HashTagName",1, IsUnique = true)]
        [MaxLength(128)]
        public string Name { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}