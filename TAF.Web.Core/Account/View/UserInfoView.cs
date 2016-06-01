namespace TAF.Mvc.View
{
    using System.Collections.Generic;

    /// <summary>
    /// 用户基本信息视图
    /// </summary>
    public class UserInfoView
    {
        public string Id
        {
            get; set;
        }

        public string LoginName
        {
            get; set;
        }

        public string FullName
        {
            get; set;
        }

        public string RoleNames
        {
            get; set;
        }

        public IList<string> RoleIds
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }
    }
}