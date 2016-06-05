namespace TAF.Mvc.Business
{
    using System;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.DataProtection;

    using TAF.Mvc.Model;

    /// <summary>
    /// 用户管理器
    /// </summary>
    public class AccountManager : UserManager<ApplicationUser>
    {
        public AccountManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static AccountManager GetFromOwinContext(IOwinContext context)
        {
            return context.GetUserManager<AccountManager>();
        }

        public static AccountManager CreateForAccount(AccountContext db)
        {
            if (db == null)
            {
                db = AccountContext.Create();
            }

            var manager = new AccountManager(new UserStore<ApplicationUser>(db));
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = AccountConfig.Instance.ValidEmail
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = AccountConfig.Instance.ComplexPassword,
                RequireDigit = AccountConfig.Instance.ComplexPassword,
                RequireLowercase = AccountConfig.Instance.ComplexPassword,
                RequireUppercase = AccountConfig.Instance.ComplexPassword,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = AccountConfig.Instance.LockoutWhenLoginFail;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            manager.RegisterTwoFactorProvider(
                "EmailCode",
                new EmailTokenProvider<ApplicationUser>
                {
                    Subject = "SecurityCode",
                    BodyFormat = "Your security code is {0}"
                });
            manager.EmailService = Ioc.Create<IIdentityMessageService>();
            manager.UserTokenProvider =
                new DataProtectorTokenProvider<ApplicationUser>(
                    new DpapiDataProtectionProvider("Sample").Create("EmailConfirmation"));
            return manager;
        }
    }
}