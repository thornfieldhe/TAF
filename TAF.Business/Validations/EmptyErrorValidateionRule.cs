// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmptyErrorValidateionRule.cs" company="">
//   
// </copyright>
// <summary>
//   空白验证规则，只需要传入需要返回的错误信息
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Validation
{
    using System.ComponentModel.DataAnnotations;

    using TAF.Core;

    /// <summary>
    /// 空白验证规则，只需要传入需要返回的错误信息
    /// </summary>
    public class EmptyErrorValidateionRule : IValidationRule
    {
        /// <summary>
        /// The error message.
        /// </summary>
        private readonly string errorMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyErrorValidateionRule"/> class.
        /// </summary>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        public EmptyErrorValidateionRule(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        public ValidationResult Validate()
        {
            return new ValidationResult(errorMessage);
        }
    }
}