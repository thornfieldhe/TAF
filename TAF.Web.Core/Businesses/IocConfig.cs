namespace TAF.MVC.Businesses
{

    using Autofac;

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
        }
    }
}