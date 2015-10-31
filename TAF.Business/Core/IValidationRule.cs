// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidationRule.cs" company="">
//   
// </copyright>
// <summary>
//   验证规则
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Core
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 验证规则
    /// </summary>
    public interface IValidationRule
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        ValidationResult Validate();
    }
}