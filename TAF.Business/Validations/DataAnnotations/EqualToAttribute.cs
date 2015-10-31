// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EqualToAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   值不相等
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.ComponentModel.DataAnnotations
{
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Validates that the property has the same value as the given 'otherProperty' 
    /// </summary>
    /// <remarks>
    /// From Mvc3 Futures
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class EqualToAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualToAttribute"/> class.
        /// </summary>
        /// <param name="otherProperty">
        /// The other property.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public EqualToAttribute(string otherProperty)
        {
            if (otherProperty == null)
            {
                throw new ArgumentNullException("otherProperty");
            }

            OtherProperty = otherProperty;
            OtherPropertyDisplayName = null;
        }

        /// <summary>
        /// Gets the other property.
        /// </summary>
        public string OtherProperty
        {
            get;
        }

        /// <summary>
        /// Gets or sets the other property display name.
        /// </summary>
        public string OtherPropertyDisplayName
        {
            get; set;
        }

        /// <summary>
        /// The format error message.
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
                ErrorMessage = "值不相等";
            }

            var otherPropertyDisplayName = OtherPropertyDisplayName ?? OtherProperty;

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, otherPropertyDisplayName);
        }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="validationContext">
        /// The validation context.
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var memberNames = new[] { validationContext.MemberName };

            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherPropertyInfo == null)
            {
                return
                    new ValidationResult(
                        string.Format(
                                      CultureInfo.CurrentCulture,
                            "找不到名称为{0}的属性",
                            OtherProperty),
                        memberNames);
            }

            var displayAttribute =
                otherPropertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as
                DisplayAttribute;

            if (displayAttribute != null && !string.IsNullOrWhiteSpace(displayAttribute.Name))
            {
                OtherPropertyDisplayName = displayAttribute.Name;
            }

            object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            if (!Equals(value, otherPropertyValue))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
            }

            return null;
        }
    }
}