// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Validator.cs" company="">
//   
// </copyright>
// <summary>
//   验证操作
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Core;
    using Utility;

    /// <summary>
    /// 验证操作
    /// </summary>
    public class Validator : IValidator
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="target">
        /// 验证目标
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResultCollection"/>.
        /// </returns>
        public ValidationResultCollection Validate(object target)
        {
            target.CheckNull("target");
            var result = new ValidationResultCollection();
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(target, null, null);
            var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(target, context, validationResults, true);
            if (!isValid)
            {
                result.AddResults(validationResults);
            }

            return result;
        }
    }
}