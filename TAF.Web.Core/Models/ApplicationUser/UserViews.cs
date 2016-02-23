namespace TAF.Web.Models
{
    using System.Collections.Generic;

    public class LoginUser
    {
        public string Name
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }
    }

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

    public class UserListView
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
    }

    public class ConfirmPassword
    {
        public string NewPassword
        {
            get; set;
        }

        public string OldPassword
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }
    }

    public class ChangedPasswordView
    {
        public string CurrentPassword
        {
            get; set;
        }

        public string NewPassword
        {
            get; set;
        }
    }
}