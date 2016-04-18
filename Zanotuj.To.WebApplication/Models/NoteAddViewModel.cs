using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Zanotuj.To.WebApplication.Models
{
    public class NoteAddViewModel
    {

        [Required]
        [Display(Name = "TYTUŁ:")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Display(Name = "TREŚĆ")]
        public string Content { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "HASH TAGI")]
        public string HashTags { get; set; }

    }

    public class NoteEditViewModel
    {
        [Required]
        [Display(Name = "TYTUŁ")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Display(Name = "TREŚĆ")]
        public string Content { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "HASH TAGI")]
        public string HashTags { get; set; }

    }
}
