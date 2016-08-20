namespace TAF.Owin
{
    using Nancy;

    /// <summary>
    /// 
    /// </summary>
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/weixin/a"] = _ =>
                           {
                               return Response.AsJson(new Bug() { Id = 1, Name = "zhangsan " });
                           };
        }
    }
}