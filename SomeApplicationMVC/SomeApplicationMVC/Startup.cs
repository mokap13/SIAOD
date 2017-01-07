using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SomeApplicationMVC.Startup))]
namespace SomeApplicationMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
