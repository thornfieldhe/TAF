// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IocConfig.cs" company="">
//   
// </copyright>
// <summary>
//   业务类基类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.MVC.Business
{
    using System.Data.Entity;

    using Autofac;

    using CacheManager.Core;

    using TAF.Data;
    using TAF.Mvc.Business;

    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public class BaseWebIocConfig : TAF.DI.ConfigBase
    {
        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="builder">
        /// 容器生成器
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var cache = CacheFactory.Build(
                "getStartedCache",
                settings =>
                    {
                        settings.WithSystemRuntimeCacheHandle("handleName")
                .And
                .WithRetryTimeout(7200000);
                    });
            builder.RegisterInstance(cache);
            builder.RegisterType<TAFContext>().As<DbContext>();
            builder.RegisterType<EFProvider>().As<IDbProvider>();
        }
    }
}