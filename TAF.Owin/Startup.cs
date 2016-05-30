namespace TAF.Owin
{
    using System.Web.Http;

    using global::Owin;

    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseMyApp();
            app.UseMyApp2();
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("bugs", "api/{Controller}");
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