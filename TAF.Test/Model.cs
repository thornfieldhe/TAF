// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Model.cs" company="">
//   
// </copyright>
// <summary>
//   The user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TAF.Test
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Entity;
    using TAF.Core;
    using Utility;
    using Validation;

    /// <summary>
    /// 最大长度验证规则
    /// </summary>
    public class ContainsHXHValidationRule : IValidationRule
    {
        /// <summary>
        /// The txt.
        /// </summary>
        private readonly string txt;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainsHXHValidationRule"/> class. 
        /// 初始化客户英文名验证规则
        /// </summary>
        /// <param name="text">
        /// 客户
        /// </param>
        public ContainsHXHValidationRule(string text)
        {
            this.txt = text;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        public ValidationResult Validate()
        {
            if (!txt.Contains("HXH"))
            {
                return new ValidationResult("姓名必须包含HXH");
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// 最小长度验证规则
    /// </summary>
    public class MinLengthValidationRule : IValidationRule
    {
        /// <summary>
        /// The txt.
        /// </summary>
        private readonly string txt;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinLengthValidationRule"/> class. 
        /// 初始化客户英文名验证规则
        /// </summary>
        /// <param name="text">
        /// 客户
        /// </param>
        public MinLengthValidationRule(string text)
        {
            this.txt = text;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        public ValidationResult Validate()
        {
            if (this.txt.IsNull() || (this.txt.Length < 2))
            {
                return new ValidationResult("姓名长度不能小于2");
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// The user.
    /// </summary>
    [Serializable]
    public class User2 : BaseBusiness<User2>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="results">
        /// The results.
        /// </param>
        protected override void Validate(ValidationResultCollection results)
        {
            results.Add(new MinLengthValidationRule(this.Name).Validate());
            base.Validate(results);
        }

        /// <summary>
        /// The add descriptions.
        /// </summary>
        protected override void AddDescriptions()
        {
            this.AddDescription("Name:" + this.Name + ",");
        }
    }

    /// <summary>
    /// The user.
    /// </summary>
    [Serializable]
    public class User : IComparable<User>, IUser
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email
        {
            get; set;
        }

        /// <summary>
        /// The compare to.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CompareTo(User other)
        {
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get; set;
        }
    }

    /// <summary>
    /// The User interface.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name
        {
            get; set;
        }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal = 1,

        /// <summary>
        /// 错误
        /// </summary>
        [OrderBy(5)]
        [Description("错误")]
        Error = 2,

        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")]
        [OrderBy(6)]
        Warning = 3,

        /// <summary>
        /// 信息
        /// </summary>
        [Description("信息")]
        Information = 4,

        /// <summary>
        /// 调试
        /// </summary>
        [OrderBy(3)]
        [Description("调试")]
        Debug = 5
    }
}