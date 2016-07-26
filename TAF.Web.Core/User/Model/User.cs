namespace TAF.Mvc.Model
{
    using System;
    using TAF.Utility;

    /// <summary>
    /// 用户
    /// </summary>
    public class User : BaseBusiness<User>
    {
        public User()
        {
        }

        public User(Guid id) : base(id) { }

        #region 属性

        private string fullName;

        private string userName;

        private string passwordHash;

        private string securityStamp;

        private bool isLocked;

        public string FullName
        {
            get
            {
                return this.fullName;
            }

            set
            {
                SetProperty(ref this.fullName, value);
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                SetProperty(ref this.userName, value);
            }
        }

        protected string PasswordHash
        {
            get
            {
                return this.passwordHash;
            }

            set
            {
                SetProperty(ref this.passwordHash, value);
            }
        }

        protected string SecurityStamp
        {
            get
            {
                return this.securityStamp;
            }

            set
            {
                SetProperty(ref this.securityStamp, value);
            }
        }

        protected bool IsLocked
        {
            get
            {
                return this.isLocked;
            }

            set
            {
                SetProperty(ref this.isLocked, value);
            }
        }
        #endregion

        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription(nameof(UserName), UserName.ToStr());
            AddDescription(nameof(FullName), UserName.ToStr());
            AddDescription(nameof(PasswordHash), PasswordHash.ToStr());
        }
        #endregion

        #region 业务方法

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User Login(string userName, string password)
        {
            var user = Find(r => r.UserName == userName);
            if(user == null) return null;
            var isValidate = Encrypt.SHA1($"{ user.SecurityStamp}{password}") == user.PasswordHash;
            return isValidate ? user : null;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="password"></param>
        /// <param name="userId"></param>
        /// <param name="submit"></param>
        /// <returns></returns>
        public User Register(string password, Guid userId, bool submit = true)
        {
            this.SecurityStamp = Encrypt.GetNewPassword(12);
            this.PasswordHash = Encrypt.SHA1($"{this.SecurityStamp}{password}");
            this.Create(userId, submit);
            return this;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool ChangePwd(string userName, string oldPassword, string newPassword, Guid userId)
        {
            var user = Find(r => r.UserName == userName);
            if (user == null || user.PasswordHash != Encrypt.SHA1($"{user.SecurityStamp}{oldPassword}"))
                return false;
            user.PasswordHash = Encrypt.SHA1($"{user.SecurityStamp}{newPassword}");
            return user.Save(userId) == 1;
        }
        #endregion
    }
}