using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportLoto.Startup))]
namespace SportLoto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
