//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="AccountBusiness.cs" company="" author="何翔华">
////   
//// </copyright>
//// <summary>
////   AccountBusiness
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------
//
//namespace TAF.Mvc.Business
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Threading.Tasks;
//
//    using AutoMapper;
//
//    using CacheManager.Core;
//
//    using Microsoft.AspNet.Identity;
//
//    using TAF.Business;
//    using TAF.Mvc.Model;
//    using TAF.Mvc.View;
//
//    /// <summary>
//    /// 
//    /// </summary>
//    public class AccountBusiness
//    {
//        #region Managers
//
//        private AccountUserManager _userManager;
//
//        public AccountUserManager UserManager
//        {
//            get
//            {
//                _userManager = _userManager ?? AccountUserManager.CreateForAccount(new TAFContext());
//                return this._userManager;
//            }
//            private set
//            {
//                _userManager = value;
//            }
//        }
//
//        private AccountRoleManager _roleManager;
//
//        public AccountRoleManager RoleManager
//        {
//            get
//            {
//                _roleManager = _roleManager ?? AccountRoleManager.CreateForAccount(new TAFContext());
//                return _roleManager;
//            }
//            private set
//            {
//                _roleManager = value;
//            }
//        }
//
//        #endregion
//
//        /// <summary>
//        /// 注册
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        public async Task<ActionResultStatus> Register(RegionInfoView model)
//        {
//            if (string.IsNullOrWhiteSpace(model.Password))
//            {
//                return new ActionResultStatus(0, 20, "密码不能为空");
//            }
//
//            var user = new ApplicationUser
//            {
//                UserName = model.Email,
//                Email = model.Email,
//                FullName = model.UserName,
//            };
//            var result = UserManager.Create(user, model.Password);
//            if (!result.Succeeded)
//            {
//                return new ActionResultStatus(0, 20, result.Errors.First());
//            }
//
//            Ioc.Create<ICacheManager<object>>().Update("Users", r => UserManager.Users.ToList());
//            if (model.Roles != null && model.Roles.Length > 0)
//            {
//                UserManager.AddToRoles(user.Id, model.Roles);
//            }
//
//            var code = this.UserManager.GenerateEmailConfirmationToken(user.Id);
//            await UserManager.ConfirmEmailAsync(user.Id, code);
//            return new ActionResultStatus();
//        }
//
//        /// <summary>
//        /// 忘记密码，发送邮箱
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        public async Task<ActionResultStatus> ForgetPassWord(ForgetPassInfoView model)
//        {
//            try
//            {
//                var business = new OperationSystemBusiness().Find(model.BusinessId);
//                var user = await UserManager.FindByEmailAsync(model.Email);
//
//                if (user == null)
//                {
//                    //提示该用户不存在
//                    return new ActionResultStatus(10, AccountHostResource.Account_UserNotExsit);
//                }
//                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
//                var sendModel = new SendEmailModel { Email = user.Email, Code = code, BusinessId = model.BusinessId };
//                string callbackUrl = GetShortUrl(sendModel, business.ResetPasswordLink);
//                //发送邮件
//                SendMailBusiness.Send(MailTempelateType.ForgetPasswordTpl, new ForgetPasswordQueryModel
//                {
//                    Link = callbackUrl,
//                    Name = ""
//                }, MailConfig.Current, new string[] { model.Email });
//
//            }
//            catch (Exception ex)
//            {
//
//                WebHelp.Logger.Error(ex);
//                return new ActionResultStatus(ex);
//            }
//            //提示发送邮件
//            return new ActionResultStatus();
//        }
//
//        /// <summary>
//        /// 登陆
//        /// </summary>
//        /// <param name="model"></param>
//        /// <param name="adminOnly"></param>
//        /// <param name="createToken">是否创建令牌缓存</param>
//        /// <returns></returns>
//        public async Task<ActionResultData<UserInfoWithTokenView>> Login(LoginInfoView model, bool adminOnly = false, bool createToken = true)
//        {
//            var roles = RoleManager.Roles.ToList();
//            var isInBusiness =
//                RoleManager.Roles.Any(r => r.BusinessId == model.BusinessId);
//            if (!isInBusiness)
//            {
//                return new ActionResultData<UserInfoWithTokenView>(null, ActionStatuses.Error, 10, AccountHostResource.Account_LoginFaild);
//            }
//
//            var user = await UserManager.FindAsync(model.UserName, model.Password);
//
//
//            if (user == null)
//            {
//                return new ActionResultData<UserInfoWithTokenView>(null, ActionStatuses.Error, 10, AccountHostResource.Account_LoginFaild);
//            }
//            var userRoleName = UserManager.GetRoles(user.Id);
//            if (adminOnly && !userRoleName.Any(r => r.Contains("Admins")))
//            {
//                return new ActionResultData<UserInfoWithTokenView>(null, ActionStatuses.Error, 10, AccountHostResource.Account_LoginFaild);
//            }
//
//            var newUser = Mapper.Map<UserInfoView>(user);
//            newUser.Roles = userRoleName;
//
//            //创建用户登陆Token，有效时间为2H
//            var tokenStr = WebHelp.CreateToken(user.Id);
//            if (createToken)
//            {
//                Ioc.Create<ICacheManager<object>>().Add(tokenStr, new AuthorisedUserView { Roles = userRoleName, UserName = user.UserName, Id = user.Id });
//            }
//
//            return new ActionResultData<UserInfoWithTokenView>(new UserInfoWithTokenView { Token = tokenStr, UserInfo = newUser });
//        }
//
//        public ActionResultStatus Logout(string token)
//        {
//            Ioc.Create<ICacheManager<object>>().Remove(token);
//            return new ActionResultStatus();
//        }
//
//        /// <summary>
//        /// 忘记密码
//        /// </summary>
//        /// <param name="email"></param>
//        /// <returns></returns>
//        public async Task<ActionResultStatus> ForgotPassword(string email)
//        {
//            var user = await UserManager.FindByEmailAsync(email);
//            if (user == null)
//            {
//                return new ActionResultStatus(0, 10, "账户不存在");
//            }
//            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
//
//            //todos 发送找回密码邮件
//            //await UserManager.SendEmailAsync(user.Id,"","");
//            return new ActionResultStatus();
//        }
//
//        /// <summary>
//        /// 修改密码
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        public async Task<ActionResultStatus> ChangePassword(ChangePwtInfoView model)
//        {
//            var result = await UserManager.ChangePasswordAsync(model.UserName, model.CurrentPassword, model.NewPassword);
//            if (result.Succeeded)
//            {
//                return new ActionResultStatus();
//            }
//            return new ActionResultStatus(10, result.Errors.First());
//        }
//
//        /// <summary>
//        /// 重置密码
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        public async Task<ActionResultStatus> ResetPassword(ResetPwtInfoView model)
//        {
//            var mouldbussiness = new MouldByCodeBusiness();
//            var Module = mouldbussiness.Find(x => x.Code == model.Code);
//            SendEmailModel sendModel = JsonHelper.DeSerializesFromString<SendEmailModel>(Module.MouldStr);
//            string Email = sendModel.Email;
//            string Code = sendModel.Code;
//            Guid businessId = sendModel.BusinessId;
//            var OperationSys = new OperationSystemBusiness().Find(businessId);
//            var user = await UserManager.FindByEmailAsync(Email);
//            //用户是否存在，用户的code是否过期判断
//            if (user == null)
//                return new ActionResultStatus(10, AccountHostResource.Account_UserNotExsit);
//            if (Module.IsOverdue)
//                return new ActionResultData<string>(OperationSys.ErrorPage, ActionStatuses.Error, 10,
//                   AccountHostResource.Account_CodeOverdueInfo);
//            //修改model的状态
//            Module.IsOverdue = true;
//            mouldbussiness.Commint();
//            //重置用户密码
//            var result = await UserManager.ResetPasswordAsync(user.Id, Code, model.Password);
//            if (result.Succeeded)
//                //密码修改成功：跳转到登录页面
//                return new ActionResultData<string>(OperationSys.AfterConfirmEmail, ActionStatuses.OK, 10,
//                   "");
//            //留在本页面显示，错误信息
//            return new ActionResultStatus(10, result.Errors.First());
//        }
//
//        /// <summary>
//        /// 初始化密码
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public async Task<ActionResultStatus> ResetPassword(string userId)
//        {
//            var user = await UserManager.FindByIdAsync(userId);
//            if (user == null)
//            {
//                return new ActionResultStatus(10, AccountHostResource.Account_UserNotExsit);
//            }
//            var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
//            await UserManager.ResetPasswordAsync(user.Id, code, "qwe123!@#");
//            return new ActionResultStatus();
//        }
//
//        /// <summary>
//        /// 分页获取用户列表
//        /// </summary>
//        /// <param name="pager"></param>
//        /// <param name="where"></param>
//        /// <returns></returns>
//        public ActionResultData<Pager<UserInfoView>> GetUsers(Pager<UserInfoView> pager, Func<ApplicationUser, bool> where)
//        {
//            var users = Ioc.Create<ICacheManager<object>>().Get("Users") as List<ApplicationUser>;
//            if (users == null)
//            {
//                users = UserManager.Users.ToList();
//                Ioc.Create<ICacheManager<object>>().Add("Users", users);
//            }
//            var result = users.Where(where)
//                    .OrderBy(r => r.FullName)
//                    .Skip(pager.PageSize * (pager.PageIndex - 1))
//                    .Take(pager.PageSize)
//                    .ToList();
//            pager.Total = users.Count(where);
//            pager.Datas = Mapper.Map<List<UserInfoView>>(result);
//            pager.GetShowIndex();
//            return new ActionResultData<Pager<UserInfoView>>(pager);
//        }
//
//        /// <summary>
//        /// 获取角色列表
//        /// </summary>
//        /// <returns></returns>
//        public ActionResultData<List<Tuple<string, Tuple<string, string>>>> GetRoles()
//        {
//            var businesses = new OperationSystemBusiness().GetAll();
//            var result = RoleManager.Roles.ToList().Select(r => new Tuple<string, Tuple<string, string>>(businesses.Single(b => b.Id == r.BusinessId).Name, new Tuple<string, string>(r.Id, r.Name))).ToList();
//            return new ActionResultData<List<Tuple<string, Tuple<string, string>>>>(result);
//        }
//
//        /// <summary>
//        /// 为用户设置角色
//        /// </summary>
//        /// <returns></returns>
//        public async Task<ActionResultStatus> SetRoles(string userId, string[] roles)
//        {
//            var user = await UserManager.FindByIdAsync(userId);
//            if (user == null)
//            {
//                return new ActionResultStatus(20, AccountHostResource.Account_UserNotExsit);
//            }
//            var oldRoles = user.Roles.Select(m => m.RoleId).ToList();
//
//            var oldRoleNames = RoleManager.Roles.Where(r => oldRoles.Contains(r.Id)).Select(r => r.Name).ToArray();
//            await UserManager.RemoveFromRolesAsync(userId, oldRoleNames);
//            var result = await UserManager.AddToRolesAsync(userId, roles);
//            if (result.Succeeded)
//            {
//                var users = UserManager.Users.ToList();
//                var cache = Ioc.Create<ICacheManager<object>>().Get("Users") as List<ApplicationUser>;
//                if (cache == null)
//                {
//                    Ioc.Create<ICacheManager<object>>().Add("Users", users);
//                }
//                else
//                {
//                    Ioc.Create<ICacheManager<object>>().Update("Users", r => users);
//                }
//                return new ActionResultStatus();
//            }
//            return new ActionResultStatus(10, result.Errors.First());
//        }
//
//        /// <summary>
//        /// 删除用户
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public async Task<ActionResultStatus> DeleteUser(string userId)
//        {
//            var user = UserManager.Users.SingleOrDefault(r => r.Id == userId);
//            if (user == null)
//            {
//                return new ActionResultStatus(20, AccountHostResource.Account_UserNotExsit);
//            }
//            var result = await UserManager.DeleteAsync(user);
//            if (result.Succeeded)
//            {
//                var users = UserManager.Users.ToList();
//
//                Ioc.Create<ICacheManager<object>>().Update("Users", r => users);
//
//                return new ActionResultStatus();
//            }
//            return new ActionResultStatus(10, result.Errors.First());
//        }
//    }
//}