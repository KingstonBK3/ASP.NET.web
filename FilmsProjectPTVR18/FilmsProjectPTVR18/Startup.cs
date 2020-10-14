using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FilmsProjectPTVR18.Startup))]
namespace FilmsProjectPTVR18
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
