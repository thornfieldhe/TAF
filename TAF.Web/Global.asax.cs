using System.Web.Mvc;
using System.Web.Routing;

namespace TAF.Web
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Optimization;

    using AutoMapper;

    using TAF.MVC.Businesses;
    using TAF.Web.Businesses;
    using TAF.Web.Models;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new BaseDbInitializer());
            //            Database.SetInitializer(new DbInitializer());
            Ioc.RegisterMvc(Assembly.GetExecutingAssembly(), new IocConfig());
            InitMap();
        }

        private void InitMap()
        {
            Mapper.CreateMap<SystemDictionaryView, SystemDictionary>();
            Mapper.CreateMap<SystemDictionary, SystemDictionaryView>();
        }
    }
}
