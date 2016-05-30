
namespace TAF.Test
{

    using Autofac;

    using TAF.Core;
    using TAF.Test;
    using TAF.Validation;

    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public class IocConfig : TAF.DI.ConfigBase
    {
        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="builder">容器生成器</param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<Validator2>().As<IValidator>();
            builder.RegisterType<ValidationHandler>().As<IValidationHandler>();
            builder.Register<IModel>(
                                 (c, p) =>
                                 {
                                     var type = p.Named<string>("type");
                                     if (type == "model1")
                                     {
                                         return new IcoTestModel1();
                                     }
                                     return new IcoTestModel2();
                                 }).As<IModel>();
        }
    }
}
