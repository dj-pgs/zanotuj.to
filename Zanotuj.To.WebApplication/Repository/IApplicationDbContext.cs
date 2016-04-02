using System.Data.Entity;
using Zanotuj.To.WebApplication.Models;

namespace Zanotuj.To.WebApplication.Repository
{
    public interface IApplicationDbContext
    {
        IDbSet<Note> Notes { get; }
        IDbSet<ApplicationUser> Users { get; }
    }
}