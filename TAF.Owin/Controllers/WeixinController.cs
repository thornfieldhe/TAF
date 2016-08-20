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
using System.Text.RegularExpressions;
using System.Web.Http;
using TAF.Mvc;
using TAF.Utility;

namespace TAF.Owin
{
    /// <summary>
    /// 
    /// </summary>
    public class WeixinController : BaseController
    {
        [AllowAnonymous]
        public HttpResponseMessage Get(string signature, string timestamp, string nonce, string echostr)
        {
            var result = Validate(signature, timestamp, nonce);
            return new HttpResponseMessage() { Content = new StringContent(echostr, Encoding.GetEncoding("UTF-8"), "application/x-www-form-urlencoded") };
        }

        [AllowAnonymous]
        public HttpResponseMessage Post()
        {
            var content = this.RequestContext.Url.Request.Content.ReadAsStringAsync().Result;
            var reg0 = new Regex(@"\[CDATA\[(.*?)\]\]");
            var reg1 = new Regex(@"<(.*?)>(\d+)<\/(.*?)>");

            var matchs0 = reg0.Matches(content);
            var matchs1 = reg1.Matches(content);
            var fromMessage = new TextMessage
            {
                ToUserName = matchs0[0].Groups[1].Value,
                FromUserName = matchs0[1].Groups[1].Value,
                MsgType = matchs0[2].Groups[1].Value,
                Content = matchs0[3].Groups[1].Value,
                CreateTime = matchs1[0].Groups[2].Value,
                MsgId = matchs1[1].Groups[2].Value,
            };

            var toMessage = $@"<xml><ToUserName><![CDATA[{ matchs0[1].Groups[1].Value}]]></ToUserName>
<FromUserName><![CDATA[{matchs0[0].Groups[1].Value}]]></FromUserName>
<CreateTime>{matchs1[0].Groups[2].Value}</CreateTime>
<MsgType><![CDATA[text]]></MsgType>
<Content><![CDATA[呵呵呵呵呵]]></Content>
</xml>";
            return new HttpResponseMessage() { Content = new StringContent(toMessage, Encoding.GetEncoding("UTF-8"), "application/x-www-form-urlencoded") };
        }

        private static bool Validate(string signature, string timestamp, string nonce)
        {
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

    public class TextMessage
    {
        public string ToUserName
        {
            get; set;
        }
        public string FromUserName
        {
            get; set;
        }
        public string CreateTime
        {
            get; set;
        }
        public string MsgType
        {
            get; set;
        }
        public string Content
        {
            get; set;
        }
        public string MsgId
        {
            get; set;
        }
    }
}