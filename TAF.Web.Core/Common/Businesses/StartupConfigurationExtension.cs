namespace TAF.Mvc.Business
{
    using System;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;

    using Owin;
    using TAF.Mvc.Model;

    public static class StartupConfigurationExtension
    {
        public static void ConfigureAuth(this IAppBuilder app)
        {
            app.CreatePerOwinContext(AccountContext.Create);
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