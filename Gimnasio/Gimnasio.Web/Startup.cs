using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gimnasio.Web.Startup))]
namespace Gimnasio.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
