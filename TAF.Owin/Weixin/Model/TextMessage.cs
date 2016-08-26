// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextMessage.cs" company="">
//   
// </copyright>
// <summary>
//   文本消息
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Owin.Model
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessage
    {
        /// <summary>
        /// 消息接收者
        /// </summary>
        public string ToUserName
        {
            get; set;
        }

        /// <summary>
        /// 消息发出者
        /// </summary>
        public string FromUserName
        {
            get; set;
        }

        /// <summary>
        /// 创建事件
        /// </summary>
        public string CreateTime
        {
            get; set;
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content
        {
            get; set;
        }

        /// <summary>
        /// Id
        /// </summary>
        public string MsgId
        {
            get; set;
        }

        /// <summary>
        /// 收到微信推送消息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static TextMessage ReceveMessage(string content)
        {
            var reg0 = new Regex(@"\[CDATA\[(.*?)\]\]");
            var reg1 = new Regex(@"<(.*?)>(\d+)<\/(.*?)>");
            var matchs0 = reg0.Matches(content);
            var matchs1 = reg1.Matches(content);
            var fromMessage = new TextMessage
            {
                ToUserName = matchs0[0].Groups[1].Value,
                FromUserName = matchs0[1].Groups[1].Value,
                Content = matchs0[3].Groups[1].Value,
                CreateTime = matchs1[0].Groups[2].Value,
                MsgId = matchs1[1].Groups[2].Value,
            };
            return fromMessage;
        }

        public  string SendMessage()
        {
            var toMessage = $@"<xml><ToUserName><![CDATA[{ FromUserName}]]></ToUserName>
                <FromUserName><![CDATA[{ToUserName}]]></FromUserName>
                <CreateTime>{CreateTime}</CreateTime>
                <MsgType><![CDATA[text]]></MsgType>
                <Content><![CDATA[{Content}]]></Content>
                </xml>";
            return toMessage;
        }
    }
}