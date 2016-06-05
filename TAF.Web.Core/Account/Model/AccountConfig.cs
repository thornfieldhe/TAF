namespace TAF.Mvc.Model
{
    using TAF.Utility;

    /// <summary>
    /// 
    /// </summary>
    public class AccountConfig : SingletonBase<AccountConfig>
    {
        /// <summary>
        /// 验证邮箱地址
        /// </summary>
        public bool ValidEmail
        {
            get; set;
        }

        /// <summary>
        /// 登录失败5次后锁定账户
        /// </summary>
        public bool LockoutWhenLoginFail
        {
            get; set;
        }

        /// <summary>
        /// 启用复杂密码
        /// </summary>
        public bool ComplexPassword
        {
            get; set;
        }

        /// <summary>
        /// 使用两步验证
        /// </summary>
        public bool UseTwoFactor
        {
            get; set;
        }
    }
}