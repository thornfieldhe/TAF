// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   最小值
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.ComponentModel.DataAnnotations
{
    using System.Globalization;

    /// <summary>
    /// The min attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MinAttribute : DataTypeAttribute
    {
        /// <summary>
        /// The _min.
        /// </summary>
        private readonly double _min;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinAttribute"/> class.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        public MinAttribute(int min)
            : base("min")
        {
            _min = min;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinAttribute"/> class.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        public MinAttribute(double min)
            : base("min")
        {
            _min = min;
        }

        /// <summary>
        /// Gets the min.
        /// </summary>
        public object Min
        {
            get
            {
                return _min;
            }
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
                ErrorMessage = "属性 {0}应大于等于属性{1}";
            }

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _min);
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

            double valueAsDouble;

            var isDouble = double.TryParse(Convert.ToString(value), out valueAsDouble);

            return isDouble && valueAsDouble >= _min;
        }
    }
}