using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zanotuj.To.WebApplication.Models
{
    public class AccountLoginViewModel
    {
        [Required]
        [Display(Name = "NICK")]
        public string Nick { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "HASŁO")]
        public string Password { get; set; }
    }
}