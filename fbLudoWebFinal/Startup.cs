using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(fbLudoWebFinal.Startup))]
namespace fbLudoWebFinal
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
