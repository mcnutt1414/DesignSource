using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DesignSource.Startup))]
namespace DesignSource
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
