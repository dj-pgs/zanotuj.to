using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zanotuj.To.WebApplication.Startup))]
namespace Zanotuj.To.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
