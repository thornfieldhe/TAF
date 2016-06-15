// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationFilterAttribute.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   AuthenticationFilterAttribute
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Businesses
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using CacheManager.Core;

    using TAF.Mvc.Log;

    /// <summary>
    ///  基本验证Attribtue，用以Action的权限处理
    /// </summary>
    public class AuthenticationFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 检查用户是否有该Action执行的操作权限
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //增加操作日志
            var log = new Log()
            {
                Action = $"{actionContext.ControllerContext.ControllerDescriptor.ControllerName}/{actionContext.ActionDescriptor.ActionName}",
                Note = GetText(actionContext.ActionArguments)
            };

            //获取当前语言状态
            SaveLanguageKey(actionContext);

            //            AuthFrom(actionContext, ref log);


            log.Create();
        }

        private void AuthFrom(HttpActionContext actionContext, ref Log log)
        {
            //            actionContext.Request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes()))








            //检验用户ticket信息，用户ticket信息来自调用发起方
            var authorizationToken = actionContext.Request.Headers.SingleOrDefault(r => r.Key == "token");
            if (!string.IsNullOrWhiteSpace(authorizationToken.Value?.First()) && Ioc.Create<ICacheManager<object>>().Get(authorizationToken.Value.First()) != null)
            {
                var authorizationUser = Ioc.Create<ICacheManager<object>>().Get(authorizationToken.Value.First());

                //判断用户是否属于当前Action要求的角色、用户范围中
                var attr = actionContext.ActionDescriptor.GetCustomAttributes<AuthorizeAttribute>();
                //                if (attr.Any(a => a != null))
                //                {
                //                    var authorizedUser = authorizationUser as AuthorisedUserView;
                //                    log.UserId = UserContext.Current.Users.Single(r => r.Email == authorizedUser.UserName).Id;
                //                    if (IsInRoles(authorizedUser, attr.First()))
                //                    {
                //                        base.OnActionExecuting(actionContext);
                //                    }
                //                    else
                //                    {
                //                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                //                    }

                //                }
                //                else
                //                {
                //                    base.OnActionExecuting(actionContext);
                //                }
            }
            else
            {
                //如果请求Header不包含ticket，则判断是否是匿名调用
                var attr = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>();

                //是匿名用户，则继续执行；非匿名用户，抛出“未授权访问”信息
                if (attr.Any(a => a != null))
                {

                    base.OnActionExecuting(actionContext);
                }

                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
        }

        //        /// <summary>
        //        /// 用户属于当前Action的角色，用户范围中
        //        /// </summary>
        //        /// <param name="authorizationUser"></param>
        //        /// <param name="attr"></param>
        //        /// <returns></returns>
        //        private bool IsInRoles(AuthorisedUserView authorizationUser, AuthorizeAttribute attr)
        //        {
        //            var inRole = true; //是否在角色内
        //            var inUser = true; //是否在用户内
        //            if (!string.IsNullOrWhiteSpace(attr.Roles))
        //            {
        //                var roles = attr.Roles.Split(',');
        //                if (!roles.Intersect(authorizationUser.Roles.ToArray()).Any())
        //                {
        //                    inRole = false;
        //                }
        //            }
        //            if (!string.IsNullOrWhiteSpace(attr.Users))
        //            {
        //                var users = attr.Users.Split(',');
        //                if (!users.Contains(authorizationUser.UserName))
        //                {
        //                    inUser = false;
        //                }
        //            }
        //            return inRole && inUser;
        //        }

        /// <summary>
        /// 创建日志参数信息
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private string GetText(Dictionary<string, object> arguments)
        {
            var result = "";
            foreach (var argument in arguments)
            {
                result += string.Format("'{0}':'{1}',", argument.Key, argument.Value);
            }
            return "{" + result.Trim(',') + "}";
        }

        /// <summary>
        /// 保存语言设置(存入到线程中)
        /// </summary>
        /// <param name="actionContext">
        /// 操作上下文
        /// </param>
        private void SaveLanguageKey(HttpActionContext actionContext)
        {

            var lang = actionContext.Request.Headers.AcceptLanguage.First().Value;
            //线程存储当前语言状态
            var cultureInfo = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
        }

    }
}