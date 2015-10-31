// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Char.cs" company="">
//   
// </copyright>
// <summary>
//   char扩展类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    using System;
    using System.Globalization;

    /// <summary>
    /// The extensions.
    /// </summary>
    public partial class Extensions
    {
        /// <summary>
        /// 比较两个字符时控制大小写敏感性
        /// </summary>
        /// <param name="firstChar">
        /// </param>
        /// <param name="secondChar">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsCharEqual(this char firstChar, char secondChar)
        {
            return IsCharEqual(firstChar, secondChar, false);
        }

        /// <summary>
        /// 比较两个字符时控制大小写敏感性
        /// </summary>
        /// <param name="firstChar">
        /// </param>
        /// <param name="secondChar">
        /// </param>
        /// <param name="caseSensitiveCompare">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsCharEqual(this char firstChar, char secondChar, bool caseSensitiveCompare)
        {
            if (caseSensitiveCompare)
            {
                return firstChar.Equals(secondChar);
            }

            return char.ToUpperInvariant(firstChar).Equals(char.ToUpperInvariant(secondChar));
        }

        /// <summary>
        /// 比较两个字符时控制大小写敏感性
        /// </summary>
        /// <param name="firstChar">
        /// </param>
        /// <param name="firstCharCulture">
        /// </param>
        /// <param name="secondChar">
        /// </param>
        /// <param name="secondCharCulture">
        /// </param>
        /// <param name="caseSensitiveCompare">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsCharEqual(
            this char firstChar, 
            CultureInfo firstCharCulture, 
            char secondChar, 
            CultureInfo secondCharCulture, 
            bool caseSensitiveCompare = false)
        {
            return caseSensitiveCompare
                       ? firstChar.Equals(secondChar)
                       : char.ToUpper(firstChar, firstCharCulture).Equals(char.ToUpper(secondChar, secondCharCulture));
        }
    }
}