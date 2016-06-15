// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicAuthenticationAttribute.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   BasicAuthenticationAttribute
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Account.Model
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    /// <summary>
    /// 基本
    /// </summary>
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return base.OnAuthorizationAsync(actionContext, cancellationToken);

        }
    }
}