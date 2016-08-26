// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CLASS.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   CLASS
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using TAF.Mvc;
using TAF.Utility;

namespace TAF.Owin.Controller
{
    using System.Linq;
    using System.Net;
    using System.Web.Http.Controllers;

    using TAF.Owin.Model;

    /// <summary>
    /// 
    /// </summary>
    public class WeixinController : BaseController
    {
        [AllowAnonymous]
        public HttpResponseMessage Get(string signature, string timestamp, string nonce, string echostr)
        {
            var result = Validate(this.RequestContext);
            return new HttpResponseMessage() { Content = new StringContent(echostr, Encoding.GetEncoding("UTF-8"), "application/x-www-form-urlencoded") };
        }

        [AllowAnonymous]
        public HttpResponseMessage Post()
        {
            if (Validate(this.RequestContext))
            {
                var content = this.RequestContext.Url.Request.Content.ReadAsStringAsync().Result;
                var message = TextMessage.ReceveMessage(content);
                message.Content = Randoms.GetRandomCode(5);
                return new HttpResponseMessage() { Content = new StringContent(message.SendMessage(), Encoding.GetEncoding("UTF-8"), "application/x-www-form-urlencoded") };
            }

            return new HttpResponseMessage() { StatusCode = HttpStatusCode.Forbidden };
        }

        private static bool Validate(HttpRequestContext context)
        {
            var query = context.Url.Request.GetQueryNameValuePairs();
            var signature = query.Single(r => r.Key == "signature").Value;
            var timestamp = query.Single(r => r.Key == "timestamp").Value;
            var nonce = query.Single(r => r.Key == "nonce").Value;
            var token = ConfigManager.Instance.GetConfig<string>("Weixin", "Token");
            var array = new string[] { token, timestamp, nonce };
            Array.Sort(array);
            var newStr = $"{array[0]}{array[1]}{array[2]}";
            var result = Encrypt.SHA1(newStr);
            LogManager.Instance.Logger.Info($"result:{result}");
            LogManager.Instance.Logger.Info($"signature:{signature}");
            LogManager.Instance.Logger.Info($"timestamp:{timestamp}");
            LogManager.Instance.Logger.Info($"nonce:{nonce}");
            return result == signature;
        }
    }


}