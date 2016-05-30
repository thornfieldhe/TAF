namespace TAF.Owin
{
    using System.Threading.Tasks;

    using Microsoft.Owin;
    using TAF.Utility;
    /// <summary>
    /// 
    /// </summary>
    public class MyMiddleware : OwinMiddleware
    {
        public MyMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            if (context.Request.Headers.ContainsKey("user"))
            {
                var bug = new Bug() { Id = 25, Name = "张三" };
                context.Response.ContentType = "application/json; charset=utf-8";
                context.Response.Write(bug.ToJsonString());
                return Task.FromResult(0);
            }
            return Next.Invoke(context);
        }
    }


    public class MyMiddleware2 : OwinMiddleware
    {
        public MyMiddleware2(OwinMiddleware next)
            : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write("abcd");
            return Next.Invoke(context);
        }
    }
}