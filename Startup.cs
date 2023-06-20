using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebWatchOnline.Startup))]
namespace WebWatchOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
