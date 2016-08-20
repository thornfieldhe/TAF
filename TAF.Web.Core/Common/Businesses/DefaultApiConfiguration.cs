// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultApiConfiguration.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   DefaultApiConfiguration
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Business
{
    using System.Web.Http;

    using TAF.Mvc.Businesses;
    using TAF.Mvc.Model;

    /// <summary>
    /// 
    /// </summary>
    public class DefaultApiConfiguration : HttpConfiguration
    {
        public DefaultApiConfiguration()
        {
            this.MapHttpAttributeRoutes();
            this.Routes.MapHttpRoute("api", "api/{Controller}");
            this.Routes.MapHttpRoute("mvc", "{Controller}/{Action}");

            this.Filters.Add(new AuthenticationFilterAttribute());
            this.Filters.Add(new ExceptionFilterAttribute());
           
            this.EnableCors(
                new System.Web.Http.Cors.EnableCorsAttribute(
                    string.Join(",", CfgLoader.Instance.GetArraryConfig<string>("Csrf", "Address")),
                    "*",
                    "GET,POST,OPTIONS"));
        }
    }
}