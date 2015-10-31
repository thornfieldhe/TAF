// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseRegValidateAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   正则验证属性基类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.ComponentModel.DataAnnotations
{
    using System.Globalization;
    using System.Text.RegularExpressions;


    using TAF.Utility;

    /// <summary>
    /// The base reg validate attribute.
    /// </summary>
    public abstract class BaseRegValidateAttribute : ValidationAttribute
    {
        protected BaseRegValidateAttribute(string message)
        {
            ErrorMessage = message;
        }

        /// <summary>
        /// Gets the pattern.
        /// </summary>
        protected abstract string Pattern
        {
            get;
        }

        /// <summary>
        /// 格式化错误消息
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = "正则验证失败";
            }

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString);
        }

        /// <summary>
        /// 是否验证通过
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="validationContext">
        /// The validation Context.
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToStr().IsEmpty())
            {
                return null;
            }

            return Regex.IsMatch(value.ToStr(), Pattern) ? null : new ValidationResult(FormatErrorMessage(string.Empty));
        }
    }
}