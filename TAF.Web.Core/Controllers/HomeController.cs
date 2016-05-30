// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="">
//   
// </copyright>
// <summary>
//   The HomeController .
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.MVC
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    using Newtonsoft.Json;

    using TAF.Business;
    using TAF.Mvc;
    using TAF.MVC.Models;
    using TAF.Utility;

    /// <summary>
    /// The HomeController controller.
    /// </summary>
    [Authorize]
    public class BaseHomeController : BaseTAFController
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
                    return Json(new ActionResultData<string>(7,"/Home/Index"), JsonRequestBehavior.AllowGet);
                default:
                    return Json(new ActionResultStatus(0,10, "用户名或密码错误"), JsonRequestBehavior.AllowGet);
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

        [Authorize(Roles = "系统管理员组")]
        public ActionResult GetUserList(UserQueryView query, int pageIndex, int pageSize = 20)
        {
            var roles = RoleManager.Roles.ToList();
            var users = UserManager.Users
                .Where(u => ((query.FullName == null || query.FullName.Trim() == string.Empty) || u.FullName.Contains(query.FullName)) &&
                ((query.LoginName == null || query.LoginName.Trim() == string.Empty) || u.UserName.Contains(query.LoginName)) &&
                ((query.RoleId == null || query.RoleId.Trim() == string.Empty) || u.Roles.Select(r => r.RoleId).Contains(query.RoleId)))
                .ToList();
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
                Datas = list.OrderBy(r => r.FullName).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList(),
                Total = list.Count
            };
            pager.GetShowIndex();

            return Json(new ActionResultData<Pager<UserInfoView>>(7,pager), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "系统管理员组")]
        public ActionResult GetUser(string userId)
        {
            var user = UserManager.FindByIdAsync(userId).Result;
            var roles = RoleManager.Roles.ToList();
            var result = new UserInfoView
            {
                FullName = user.FullName,
                Id = user.Id,
                LoginName = user.UserName,
                RoleIds = user.Roles.Select(r => r.RoleId).ToList(),
                RoleNames = string.Join(
                                                      ",",
                                      roles.Where(r => user.Roles.Select(ur => ur.RoleId).Contains(r.Id)).Select(r => r.Name).ToList())
            };
            return Json(new ActionResultData<UserInfoView>(7,result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangedPasswordView changedPassword)
        {
            var result = await UserManager.ChangePasswordAsync(
                                                         User.Identity.GetUserId(),
                        changedPassword.CurrentPassword,
                        changedPassword.NewPassword);
            return Json(result.Succeeded ? new ActionResultData<string>(7,"密码修改成功！") : new ActionResultStatus(7,10, result.Errors.First()), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "系统管理员组")]
        public ActionResult SaveUser(UserInfoView item)
        {
            return string.IsNullOrWhiteSpace(item.Id) ? this.CreateUser(item) : this.UpdateUser(item);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private ActionResult CreateUser(UserInfoView item)
        {
            var user = UserManager.FindByName(item.LoginName);
            if (user != null)
            {
                return Json(new ActionResultStatus(0,10, "用户已存在！"));
            }
            user = new ApplicationUser
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
            item.Password = "11111111";
            UserManager.Create(user, item.Password);
            return Json(new ActionResultStatus(7));
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="infoView"></param>
        /// <returns></returns>
        private ActionResult UpdateUser(UserInfoView infoView)
        {
            var user = UserManager.FindByName(infoView.LoginName);
            if (user == null)
            {
                return Json(new ActionResultStatus(0,10, "用户不存在！"));
            }
            user.UserName = infoView.LoginName;
            user.FullName = infoView.FullName;
            user.Roles.Clear();
            if (infoView.RoleIds != null)
            {
                foreach (var roleId in infoView.RoleIds)
                {
                    user.Roles.Add(new IdentityUserRole { RoleId = roleId, UserId = user.Id });
                }
            }

            UserManager.Update(user);
            //            if (!string.IsNullOrWhiteSpace(infoView.Password))
            //            {
            //                var token = UserManager.GeneratePasswordResetToken(infoView.Id);
            //                var result = UserManager.ResetPassword(infoView.Id, token, infoView.Password);
            //                return
            //                    Json(
            //                         !result.Succeeded
            //                             ? new ActionResultStatus(10, result.Errors.First())
            //                             : new ActionResultStatus(),
            //                        JsonRequestBehavior.AllowGet);
            //            }

            return Json(new ActionResultStatus(7));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "系统管理员组")]
        public ActionResult DeleteUser(string id)
        {
            var user = UserManager.FindById(id);
            if (user == null)
            {
                return Json(new ActionResultStatus(7,10, "用户不存在！"), JsonRequestBehavior.AllowGet);
            }
            UserManager.Delete(user);
            return Json(new ActionResultStatus(7), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取系统资源
        /// </summary>
        /// <returns></returns>
        public ActionResult GetResources()
        {
            dynamic resource = new ExpandoObject();
            resource.Roles = RoleManager.Roles.ToList();
            return Content(JsonConvert.SerializeObject(resource));
        }
    }
}