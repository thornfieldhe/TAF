using Microsoft.Owin;

[assembly: OwinStartup(typeof(TAF.Web.Startup))]
namespace TAF.Web
{
    using Owin;

    public  class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          new TAFStartup().ConfigureAuth(app);
        }
    }
}