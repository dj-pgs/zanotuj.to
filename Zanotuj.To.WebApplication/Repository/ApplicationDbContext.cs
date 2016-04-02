using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Zanotuj.To.WebApplication.Models;

namespace Zanotuj.To.WebApplication.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public IDbSet<Note> Notes { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}