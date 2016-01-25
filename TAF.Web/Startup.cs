using Microsoft.Owin;

[assembly: OwinStartup(typeof(TAF.Web.Startup))]
namespace TAF.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}