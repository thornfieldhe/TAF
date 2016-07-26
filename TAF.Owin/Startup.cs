namespace TAF.Owin
{

    using global::Owin;

    using TAF.Mvc.Business;
    using TAF.MVC.Business;

    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Ioc.Register(new BaseWebIocConfig());

            //初始化数据
            InitData.Instance.DatabaseMigrate();

            //            app.UseMyApp();
            //            app.UseMyApp2();
            var config = new DefaultApiConfiguration();
            app.UseWebApi(config);
            app.UseNancy();
        }
    }

    /// <summary>
    /// 这个类是为AppBuilder添加一个名叫UseMyApp的扩展方法
    /// </summary>
    public static class MyExtension
    {
        public static IAppBuilder UseMyApp(this IAppBuilder builder)
        {
            return builder.Use<MyMiddleware>();
        }

        public static IAppBuilder UseMyApp2(this IAppBuilder builder)
        {
            return builder.Use<MyMiddleware2>();
        }
    }
}