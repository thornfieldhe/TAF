
namespace TAF.BusinessEntity.Test
{
    using System.Data.Entity;
    using Autofac;

    using EntityFramework.Filters;

    using TAF.Core;
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
            builder.RegisterType<Validator>().As<IValidator>();
            builder.RegisterType<ValidationHandler>().As<IValidationHandler>();
            builder.RegisterType<TestContexstWapper>().As<IContextWapper>();
        }
    }

    public class TestContexstWapper : IContextWapper
    {
        public DbContext Context
        {
            get
            {
                var context = new TestDbContext();
                 context.EnableFilter("Status");
                return context;
            }
        }
    }
}
