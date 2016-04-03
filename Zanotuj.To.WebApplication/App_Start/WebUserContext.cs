using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Zanotuj.To.WebApplication.Services;

namespace Zanotuj.To.WebApplication.App_Start
{
    public class WebUserContext:IUserContext
    {
        public string Nick { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public string ProfilePhotoUrl { get; private set; }
        public string UserId { get; private set; }

        public WebUserContext ()
        {
            var ctx = HttpContext.Current.GetOwinContext();
            Nick = ctx.Authentication.User.Identity.Name;
            IsAuthenticated = ctx.Authentication.User.Identity.IsAuthenticated;
            var photoClaim = ctx.Authentication.User.Claims.FirstOrDefault(c => c.Type == "profile:photo:url");
            if (photoClaim != null)
            {
                ProfilePhotoUrl = photoClaim.Value;
            }

            UserId = ctx.Authentication.User.Identity.GetUserId();
        }
    }
}