using Microsoft.Owin;

[assembly: OwinStartup(typeof(TAF.Web.Startup))]
namespace TAF.Web
{
    using Owin;
    using TAF.Mvc.Business;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.ConfigureAuth();
        }
    }
}