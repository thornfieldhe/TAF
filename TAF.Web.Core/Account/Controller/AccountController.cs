// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   AccountController
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Account.Controller
{
    using System.Net.Http;
    using System.Security.Claims;
    using System.Web.Http;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class AccountController : ApiController
    {
        [RoutePrefix("api/data")]
        [Authorize]
        public class DataController : ApiController
        {
            [Route("")]
            public IHttpActionResult Get()
            {
                var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var userName = principal.Claims.Where(c => c.Type == "sub").Single().Value;
                return Ok("You are allowed to request data");
            }
        }
    }
}