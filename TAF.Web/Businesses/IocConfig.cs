namespace TAF.Web.Businesses
{
    using System.Data.Entity;

    using Autofac;

    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public class IocConfig : BaseWebIocConfig
    {
        protected override void LoadDb(ContainerBuilder builder)
        {
            builder.RegisterType<WebDbContext>().As<DbContext>();
        }
    }
}