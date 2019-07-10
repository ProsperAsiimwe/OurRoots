using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OurRoots.WebUI.Startup))]
namespace OurRoots.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
