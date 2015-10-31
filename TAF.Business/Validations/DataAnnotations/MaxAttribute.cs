// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaxAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   最大值
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.ComponentModel.DataAnnotations
{
    using System.Globalization;

    /// <summary>
    /// The max attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxAttribute : DataTypeAttribute
    {
        /// <summary>
        /// The _max.
        /// </summary>
        private readonly double _max;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxAttribute"/> class.
        /// </summary>
        /// <param name="max">
        /// The max.
        /// </param>
        public MaxAttribute(int max)
            : base("max")
        {
            _max = max;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxAttribute"/> class.
        /// </summary>
        /// <param name="max">
        /// The max.
        /// </param>
        public MaxAttribute(double max)
            : base("max")
        {
            _max = max;
        }

        /// <summary>
        /// Gets the max.
        /// </summary>
        public object Max
        {
            get
            {
                return _max;
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
                ErrorMessage = "属性 {0}应小于等于属性{1}";
            }

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _max);
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

            return isDouble && valueAsDouble <= _max;
        }
    }
}