// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidationEntity.cs" company="">
//   
// </copyright>
// <summary>
//   验证对象
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Core
{
    /// <summary>
    /// The ValidationEntity interface.
    /// </summary>
    public interface IValidationEntity
    {
        /// <summary>
        /// The add validation rule.
        /// </summary>
        /// <param name="rule">
        /// The rule.
        /// </param>
        void AddValidationRule(IValidationRule rule);

        /// <summary>
        /// The validate.
        /// </summary>
        void Validate();

        /// <summary>
        /// The is validated.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsValidated
        {
            get;
        }
    }
}