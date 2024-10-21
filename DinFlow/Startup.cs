using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DinFlow.Startup))]
namespace DinFlow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
