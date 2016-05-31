namespace TAF.Mvc
{
    using System;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;

    using Owin;

    using TAF.Mvc.Businesses;
    using TAF.Mvc.Model;

    public class TAFStartup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(TAFDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/Home/Login"),
                    Provider =
                            new CookieAuthenticationProvider
                            {
                                OnValidateIdentity =
                                        SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                                            TimeSpan.FromMinutes(30),
                                            (manager, user) => user.GenerateUserIdentityAsync(manager))
                            }
                });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }
    }
}