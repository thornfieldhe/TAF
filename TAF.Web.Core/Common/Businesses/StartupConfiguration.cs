namespace TAF.Mvc.Business
{
    using System;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    public static class StartupConfiguration
    {
        public static void ConfigureAuth(this IAppBuilder app)
        {
            app.CreatePerOwinContext(TAFContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);



            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider()
            };
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }


}