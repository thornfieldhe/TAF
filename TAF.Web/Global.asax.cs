using System.Web.Mvc;
using System.Web.Routing;

namespace TAF.Web
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Optimization;

    using AutoMapper;

    using TAF.MVC.Businesses;
    using TAF.Utility;
    using TAF.Web.Businesses;
    using TAF.Web.Models;
    using TAF.Web.Views;

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

            //商品对象映射
            Mapper.CreateMap<Product, ProductItemView>()
            .ForMember(r => r.ProductionDate, m => m.MapFrom(n => n.ProductionDate.ToShortDateString()));
            Mapper.CreateMap<ProductItemView, Product>()
            .ForMember(r => r.ProductionDate, m => m.MapFrom(n => n.ProductionDate.ToDate()));
            Mapper.CreateMap<Product, ProductListView>()
            .ForMember(r => r.Category, m => m.MapFrom(n => n.Category.Value))
            .ForMember(r => r.Color, m => m.MapFrom(n => n.Color.Value))
            .ForMember(r => r.ProductionDate, m => m.MapFrom(n => n.ProductionDate.ToShortDateString()));

            //文章对象映射
            Mapper.CreateMap<Article, ArticleItemView>()
            .ForMember(r => r.PublishDate, m => m.MapFrom(n => n.PublishDate.ToShortDateString()));
            Mapper.CreateMap<ArticleItemView, Article>()
            .ForMember(r => r.PublishDate, m => m.MapFrom(n => n.PublishDate.ToDate()));
            Mapper.CreateMap<Article, ArticleListView>()
            .ForMember(r => r.PublishDate, m => m.MapFrom(n => n.PublishDate.ToShortDateString()))
            .ForMember(r => r.Category, m => m.MapFrom(n => n.Category.Value));
        }
    }
}
