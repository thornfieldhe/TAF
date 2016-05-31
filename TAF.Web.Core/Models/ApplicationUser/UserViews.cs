namespace TAF.MVC.View
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

    public class UserQueryView
    {
        public string LoginName
        {
            get; set;
        }

        public string FullName
        {
            get; set;
        }

        public string RoleId
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