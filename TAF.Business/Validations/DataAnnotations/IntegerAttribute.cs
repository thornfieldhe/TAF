// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntegerAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   必须为int
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// The integer attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IntegerAttribute : DataTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerAttribute"/> class.
        /// </summary>
        public IntegerAttribute()
            : base("integer")
        {
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
                ErrorMessage = "应为数字";
            }

            return base.FormatErrorMessage(name);
        }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            int retNum;

            return int.TryParse(Convert.ToString(value), out retNum);
        }
    }
}