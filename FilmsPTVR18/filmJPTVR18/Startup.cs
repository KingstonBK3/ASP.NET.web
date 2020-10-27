using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(filmJPTVR18.Startup))]
namespace filmJPTVR18
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
