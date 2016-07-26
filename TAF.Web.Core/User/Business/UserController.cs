// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   AccountController
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Controller
{
    using System;
    using System.Web.Http;

    using AutoMapper;

    using CacheManager.Core;

    using TAF.Business;
    using TAF.Mvc.View;
    using TAF.Utility;

    using Account = Model.User;

    /// <summary>
    /// 
    /// </summary>
    public class UserController : BaseController<Account, Account, Account>
    {
        public class DataController : ApiController
        {
            [AllowAnonymous]
            public ActionResultData<AuthorisedUserView> Login(string userName, string password)
            {
                var user = Account.Login(userName, password);
                if (user == null)
                {
                    return new ActionResultData<AuthorisedUserView>(0, 100, "用户名或密码不正确");
                }

                var userWithToken = Mapper.Map<AuthorisedUserView>(user);
                var token = Encrypt.Md5By32(user.UserName + Encrypt.GetNewPassword(12) + DateTime.Now.ToString());
                Ioc.Create<ICacheManager<object>>().Add(token, Mapper.Map<AuthorisedUserView>(user));
                return new ActionResultData<AuthorisedUserView>(4, userWithToken);
            }
        }
    }
}