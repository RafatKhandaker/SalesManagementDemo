using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalesManagement.UI.Startup))]
namespace SalesManagement.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
