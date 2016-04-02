using System;
using System.Data.Entity;
using System.Diagnostics;
using Microsoft.AspNet.Identity.EntityFramework;
using Zanotuj.To.WebApplication.Models;

namespace Zanotuj.To.WebApplication.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public IDbSet<Note> Notes { get; set; }

        public IDbSet<HashTag>  HashTags { get; set; } 

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
#if DEBUG
            Database.Log =(entry)=>Debug.WriteLine(entry);
#endif
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}