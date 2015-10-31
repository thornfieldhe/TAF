// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   手机号验证
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.ComponentModel.DataAnnotations
{
    using TAF.Utility;

    /// <summary>
    /// 手机号验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MobilePhoneAttribute : BaseRegValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MobilePhoneAttribute"/> class.
        /// </summary>
        public MobilePhoneAttribute()
            : base("应为手机号码")
        {
        }

        /// <summary>
        /// Gets the pattern.
        /// </summary>
        protected override string Pattern
        {
            get
            {
                return StringRegExpression.Mobile;
            }
        }
    }

    /// <summary>
    /// 手机号验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailAttribute : BaseRegValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAttribute"/> class.
        /// </summary>
        public EmailAttribute()
            : base("应为邮箱地址")
        {
        }

        /// <summary>
        /// Gets the pattern.
        /// </summary>
        protected override string Pattern
        {
            get
            {
                return StringRegExpression.Email;
            }
        }
    }

    /// <summary>
    /// 整数
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class InteAttribute : BaseRegValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InteAttribute"/> class.
        /// </summary>
        public InteAttribute()
            : base("应为整数")
        {
        }

        /// <summary>
        /// Gets the pattern.
        /// </summary>
        protected override string Pattern
        {
            get
            {
                return StringRegExpression.Integer;
            }
        }
    }

    /// <summary>
    /// 数字和字母
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LetterAndNumbersAttribute : BaseRegValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LetterAndNumbersAttribute"/> class.
        /// </summary>
        public LetterAndNumbersAttribute()
            : base("应为数字和字母")
        {
        }

        /// <summary>
        /// Gets the pattern.
        /// </summary>
        protected override string Pattern
        {
            get
            {
                return StringRegExpression.LetterAndNumbers;
            }
        }
    }
}