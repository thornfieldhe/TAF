// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Validation2.cs" company="">
//   
// </copyright>
// <summary>
//   验证操作
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    using TAF.Core;
    using TAF.Utility;

    /// <summary>
    /// 验证操作
    /// </summary>
    public class Validator2 : IValidator
    {
        /// <summary>
        /// 结果
        /// </summary>
        private readonly ValidationResultCollection _result;

        /// <summary>
        /// 验证目标
        /// </summary>
        private object _target;

        /// <summary>
        /// Initializes a new instance of the <see cref="Validator2"/> class. 
        /// 初始化验证操作
        /// </summary>
        public Validator2()
        {
            _result = new ValidationResultCollection();
        }

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
            _target = target;
            var type = target.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                ValidateProperty(property);
            }

            return _result;
        }

        /// <summary>
        /// The is validate.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsValidate(object target)
        {
            var isValidated = true;
            target.CheckNull("target");
            _target = target;
            var type = target.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                ValidateProperty(property).IfFalse(() => isValidated = false);
            }

            return isValidated;
        }

        /// <summary>
        /// 验证属性
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ValidateProperty(PropertyInfo property)
        {
            var isValidated = true;
            var attributes = property.GetCustomAttributes(typeof(ValidationAttribute), true);
            foreach (var attribute in attributes)
            {
                var validationAttribute = attribute as ValidationAttribute;
                if (validationAttribute == null)
                {
                    continue;
                }

                ValidateAttribute(property, validationAttribute).IfFalse(() => isValidated = false);
            }

            return isValidated;
        }

        /// <summary>
        /// 验证特性
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="attribute">
        /// The attribute.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ValidateAttribute(PropertyInfo property, ValidationAttribute attribute)
        {
            var isValid = attribute.IsValid(property.GetValue(_target));
            if (isValid)
            {
                return true;
            }

            _result.Add(new ValidationResult(GetErrorMessage(attribute)));
            return false;
        }

        /// <summary>
        /// 获取错误消息
        /// </summary>
        /// <param name="attribute">
        /// The attribute.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetErrorMessage(ValidationAttribute attribute)
        {
            return !string.IsNullOrEmpty(attribute.ErrorMessage) ? attribute.ErrorMessage : string.Empty;
        }
    }
}