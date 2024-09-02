using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Webmvccolegio.Startup))]
namespace Webmvccolegio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
