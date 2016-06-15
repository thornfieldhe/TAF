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

    using TAF.Mvc.Model;

    /// <summary>
    /// 
    /// </summary>
    public class DefaultApiConfiguration : HttpConfiguration
    {
        public DefaultApiConfiguration()
        {
            //            this.MapHttpAttributeRoutes();
            //            this.Routes.MapHttpRoute("default", "{Controller}");
            //            this.Filters.Add(new AuthenticationFilterAttribute());
            //            this.Filters.Add(new ExceptionFilterAttribute());

            this.EnableCors(
                new System.Web.Http.Cors.EnableCorsAttribute(
                    string.Join(",", CfgLoader.Instance.GetArraryConfig<string>("Csrf", "Address")),
                    "*",
                    "GET,POST,OPTIONS"));
        }
    }
}