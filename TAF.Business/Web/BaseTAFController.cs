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
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;

    /// <summary>
    /// The BasicAuthorizeController controller.
    /// </summary>
    public class BaseTAFController : Controller
    {
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}