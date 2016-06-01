namespace TAF.MVC.Businesses
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using TAF.Mvc;
    using TAF.Mvc.Businesses;
    using TAF.Mvc.Model;

    /// <summary>
    /// 
    /// </summary>
    public class BaseDbInitializer : CreateDatabaseIfNotExists<AccountDbContext>
    {
        private const string SaUserId = "76edf148-3e31-4e9e-8cf8-f17d3c96f05f";
        private const string SaUserName = "sa";
        private const string SaPassword = "11111111";
        private const string Admins = "系统管理员组";
        private const string Users = "用户组";


        protected override void Seed(AccountDbContext context)
        {
            base.Seed(context);
            var fullName = "系统管理员";
            var roleNames = new string[] { Admins, Users };
            using (var roleManager = ApplicationRoleManager.CreateForEF(context))
            {
                var applicationRoleManager = roleManager;
                if ((from item in roleNames
                     let role = roleManager.FindByName(item)
                     where role == null
                     select new IdentityRole(item)
                     into role
                     where applicationRoleManager != null
                     select applicationRoleManager.Create(role)).Any(roleresult => !roleresult.Succeeded))
                {
                    throw new Exception("初始化系统失败！");
                }
            }

            using (var userManager = ApplicationUserManager.CreateForEF(context))
            {
                var user = userManager.FindByName(SaUserName);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        Id = SaUserId,
                        UserName = SaUserName,
                        EmailConfirmed = false,
                        FullName = fullName,
                        TwoFactorEnabled = true,
                    };
                    userManager.Create(user, SaPassword);
                    userManager.SetLockoutEnabled(user.Id, true);
                }

                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(Admins))
                {
                    userManager.AddToRole(user.Id, Admins);
                }
            }
        }
    }
}