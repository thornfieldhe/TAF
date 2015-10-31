// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeRequiredAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   日期必填
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// The date time required attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DateTimeRequiredAttribute : DataTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRequiredAttribute"/> class.
        /// </summary>
        public DateTimeRequiredAttribute()
            : base(DataType.Date)
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
                ErrorMessage = "日期不能为空";
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

            DateTime retDate;

            return DateTime.TryParse(Convert.ToString(value), out retDate);
        }
    }
}