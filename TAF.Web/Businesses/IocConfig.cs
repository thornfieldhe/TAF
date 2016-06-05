namespace TAF.Web.Businesses
{
    using System.Data.Entity;

    using Autofac;

    using TAF.Data;
    using TAF.MVC.Business;

    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public class IocConfig : BaseWebIocConfig
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebDbContext>().As<DbContext>();
            builder.RegisterType<EFProvider>().As<IDbProvider>();
        }
    }
}