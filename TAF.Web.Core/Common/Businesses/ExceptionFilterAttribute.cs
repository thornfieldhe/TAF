// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionFilterAttribute.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   ExceptionFilterAttribute
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Businesses
{

    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    using TAF.Business;

    public class ExceptionFilterAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            var result = new ActionResultStatus(0, actionExecutedContext.Exception);
            LogManager.Instance.Logger.Error(actionExecutedContext.Exception);

            // 重新打包回传的讯息
            actionExecutedContext.Response = actionExecutedContext.Request
                .CreateResponse(HttpStatusCode.BadRequest, result);
        }
    }
}