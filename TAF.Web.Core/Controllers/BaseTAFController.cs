// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicAuthorizeController.cs" company="">
//   
// </copyright>
// <summary>
//   The BasicAuthorizeController .
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;

    using TAF.Business;

    /// <summary>
    /// The BasicAuthorizeController controller.
    /// </summary>
    public class BaseTAFController : Controller
    {
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            var allowAnonymous = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Any();
            if (!allowAnonymous && !this.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Login");
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            LogManager.Instance.Logger.Error(filterContext.Exception);
            filterContext.Result = new JsonResult { Data = new ActionResultStatus(0, filterContext.Exception), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}