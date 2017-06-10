using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Sharit.Azure.Backend.Startup))]

namespace Sharit.Azure.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}