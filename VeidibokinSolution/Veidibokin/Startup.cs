using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Veidibokin.Startup))]
namespace Veidibokin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
