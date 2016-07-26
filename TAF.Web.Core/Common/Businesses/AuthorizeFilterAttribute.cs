namespace TAF.Mvc.Businesses
{
    using System.Web.Http;
    using System.Web.Http.Controllers;

    public class AuthorizeFilterAttribute : AuthorizeAttribute
    {
        public AuthorizeFilterAttribute(string roles = "", string users = "")
        {
            this.Roles = roles;
            this.Users = users;
        }

        public override bool IsDefaultAttribute()
        {
            return true;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return true;
        }
    }
}
