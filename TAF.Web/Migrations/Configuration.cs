//namespace TAF.Web.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;
//    using System.Linq;
//
//    using Microsoft.AspNet.Identity;
//    using Microsoft.AspNet.Identity.EntityFramework;
//
//    using TAF.Mvc;
//    using TAF.Mvc.Model;
//    using TAF.Web.Businesses;
//
//    internal sealed class Configuration : DbMigrationsConfiguration<WebDbContext>
//    {
//        public const string _saUserId = "76edf148-3e31-4e9e-8cf8-f17d3c96f05f";
//        public const string _saUserName = "sa";
//        public const string _saPassword = "11111111";
//        public const string _admins = "系统管理员组";
//        public const string _users = "用户组";
//
//        public Configuration()
//        {
//            AutomaticMigrationsEnabled = true;
//        }
//
//        protected override void Seed(WebDbContext context)
//        {
//            base.Seed(context);
//            const string fullName = "系统管理员";
//            var roleNames = new string[] { _admins, _users };
//            using (var roleManager = ApplicationRoleManager.CreateForEF(context))
//            {
//                var applicationRoleManager = roleManager;
//                if ((from item in roleNames
//                     let role = roleManager.FindByName(item)
//                     where role == null
//                     select new IdentityRole(item)
//                     into role
//                     where applicationRoleManager != null
//                     select applicationRoleManager.Create(role)).Any(roleresult => !roleresult.Succeeded))
//                {
//                    throw new Exception("初始化系统失败！");
//                }
//            }
//
//            using (var userManager = ApplicationUserManager.CreateForEF(context))
//            {
//                var user = userManager.FindByName(_saUserName);
//                if (user == null)
//                {
//                    user = new ApplicationUser
//                    {
//                        Id = _saUserId,
//                        UserName = _saUserName,
//                        EmailConfirmed = false,
//                        FullName = fullName,
//                        TwoFactorEnabled = true,
//                    };
//                    userManager.Create(user, _saPassword);
//                    userManager.SetLockoutEnabled(user.Id, true);
//                }
//
//                var rolesForUser = userManager.GetRoles(user.Id);
//                if (!rolesForUser.Contains(_admins))
//                {
//                    userManager.AddToRole(user.Id, _admins);
//                }
//            }
//        }
//    }
//}
