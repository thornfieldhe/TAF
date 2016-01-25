﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="">
//   
// </copyright>
// <summary>
//   The HomeController .
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    using TAF.Mvc;
    using TAF.Utility;
    using TAF.Web.Models;

    /// <summary>
    /// The HomeController controller.
    /// </summary>
    [Authorize]
    public class HomeController : BaseTAFController
    {
        #region manager
        /// <summary>
        /// Gets the sign in manager.
        /// </summary>
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                signInManager = value;
            }
        }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                userManager = userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                return userManager;
            }

            private set
            {
                userManager = value;
            }
        }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return roleManager ?? ApplicationRoleManager.CreateForEF();
            }

            private set
            {
                roleManager = value;
            }
        }

        /// <summary>
        /// 授权管理器
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// The _sign in manager.
        /// </summary>
        private ApplicationSignInManager signInManager;

        /// <summary>
        /// The _user manager.
        /// </summary>
        private ApplicationUserManager userManager;

        /// <summary>
        /// The _role manager.
        /// </summary>
        private ApplicationRoleManager roleManager;
        #endregion

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var user = UserManager.Users.SingleOrDefault(r => r.UserName == User.Identity.Name);
            return user == null ? View("Login") : View(user);
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return PartialView("_Dashboard");
        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginUser user)
        {
            var result =
                await SignInManager.PasswordSignInAsync(user.Name, user.Password, true, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return Json(new ActionResultData<string>("/Home/Index"), JsonRequestBehavior.AllowGet);
                default:
                    return Json(new ActionResultStatus(10, "用户名或密码错误"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SignOut()
        {
            AuthenticationManager.SignOut();
            return View("Login");
        }

        public ActionResult ChangePasswordIndex()
        {
            return PartialView("_ChangePassword");
        }

        public ActionResult UserIndex()
        {
            var roles = RoleManager.Roles.ToList();
            return PartialView("_UserIndex", roles);
        }

        [Authorize(Roles = "Admins")]
        public ActionResult GetUserList(int pageIndex, int pageSize = 20)
        {
            var roles = RoleManager.Roles.ToList();
            var users = UserManager.Users.ToList();
            var list = new List<UserInfoView>();
            users.ForEach(
                          u =>
                          {
                              var user = new UserInfoView
                              {
                                  FullName = u.FullName,
                                  Id = u.Id,
                                  LoginName = u.UserName,
                                  RoleIds = u.Roles.Select(r => r.RoleId).ToList(),
                                  RoleNames = string.Join(
                                                      ",",
                                      roles.Where(r => u.Roles.Select(ur => ur.RoleId).Contains(r.Id)).Select(r => r.Name).ToList())
                              };
                              list.Add(user);
                          });
            var pager = new Pager<UserInfoView>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Datas = list,
                Total = list.Count
            };
            pager.GetShowIndex();

            return Json(pager, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangedPasswordView changedPassword)
        {
            var result = await UserManager.ChangePasswordAsync(
                                                         User.Identity.GetUserId(),
                        changedPassword.CurrentPassword,
                        changedPassword.NewPassword);
            return Json(result.Succeeded ? new ActionResultData<string>("密码修改成功！") : new ActionResultStatus(10, result.Errors.First()), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admins")]
        public ActionResult EditUser(string userId)
        {
            var user = UserManager.FindById(userId);
            ViewBag.Roles = RoleManager.Roles.ToList();
            return PartialView("_EditUser", user ?? new ApplicationUser() { Id = string.Empty });
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admins")]
        public ActionResult CreateUser(UserInfoView item)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = item.LoginName,
                EmailConfirmed = false,
                FullName = item.FullName,
                TwoFactorEnabled = true,
            };
            foreach (var roleName in item.RoleIds.AsList())
            {
                user.Roles.Add(new IdentityUserRole() { RoleId = roleName, UserId = user.Id });
            }

            if (string.IsNullOrWhiteSpace(item.Password))
            {
                return Json(new ActionResultStatus(10, "密码不能为空！"), JsonRequestBehavior.AllowGet);
            }

            UserManager.Create(user, item.Password);
            return Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="infoView"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admins")]
        public ActionResult UpdateUser(UserInfoView infoView)
        {
            var user = UserManager.FindByName(infoView.LoginName);
            if (user == null)
            {
                return Json(new ActionResultStatus(10, "用户不存在！"), JsonRequestBehavior.AllowGet);
            }

            user.UserName = infoView.LoginName;
            user.FullName = infoView.FullName;
            user.Roles.Clear();
            foreach (var roleId in infoView.RoleIds)
            {
                user.Roles.Add(new IdentityUserRole { RoleId = roleId, UserId = user.Id });
            }

            UserManager.Update(user);
            if (!string.IsNullOrWhiteSpace(infoView.Password))
            {
                var token = UserManager.GeneratePasswordResetToken(infoView.Id);
                var result = UserManager.ResetPassword(infoView.Id, token, infoView.Password);
                return
                    Json(
                         !result.Succeeded
                             ? new ActionResultStatus(10, result.Errors.First())
                             : new ActionResultStatus(),
                        JsonRequestBehavior.AllowGet);
            }

            return Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Admins")]
        public ActionResult DeleteUser(string id)
        {
            var user = UserManager.FindById(id);
            if (user == null)
            {
                return Json(new ActionResultStatus(10, "用户不存在！"), JsonRequestBehavior.AllowGet);
            }

            UserManager.Delete(user);
            return Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
        }
    }
}