// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleAuthorizationServerProvider.cs" company="" author="何翔华">
//   基础授权服务
// </copyright>
// <summary>
//   SimpleAuthorizationServerProvider
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Business
{
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security.OAuth;

    /// <summary>
    /// 基础授权服务
    /// </summary>
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;

            if (context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                // validate the client Id and secret against database or from configuration file.  
                context.Validated();
            }
            else
            {
                context.SetError("invalid_client", "Client credentials could not be retrieved from the Authorization header");
                context.Rejected();
            }
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<AccountUserManager>();
            var user = await userManager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("inalid_grant", "用户名或密码不正确");
                context.Rejected();
            }
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);
            context.Validated(identity);
        }
    }
}