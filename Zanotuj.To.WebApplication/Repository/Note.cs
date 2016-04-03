using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zanotuj.To.WebApplication.Models;

namespace Zanotuj.To.WebApplication.Repository
{
    public class Note : BusinessModel
    {
        public string NoteContent { get; set; }

        public string Title { get; set; }

        [MaxLength(128)]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser User { get; set; }

        public ICollection<HashTag> HashTags { get; set; }

        public Note()
        {
            HashTags=new List<HashTag>();
        }
    }
}