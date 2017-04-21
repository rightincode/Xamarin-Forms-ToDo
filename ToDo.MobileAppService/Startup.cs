using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ToDo.MobileAppService.Startup))]

namespace ToDo.MobileAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}