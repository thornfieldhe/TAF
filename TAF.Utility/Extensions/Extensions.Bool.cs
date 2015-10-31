// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Bool.cs" company="">
//   
// </copyright>
// <summary>
//   bool扩展类
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TAF.Utility
{
    using System;

    /// <summary>
    /// Set of very useful extension methods for hour by hour use in .NET code.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 非
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Not(this bool @this)
        {
            return !@this;
        }

        /// <summary>
        /// 且
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="right">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool And(this bool @this, bool right)
        {
            return @this && right;
        }

        /// <summary>
        /// 且
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="action">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool And(this bool @this, Func<bool> action)
        {
            return @this && action();
        }

        /// <summary>
        /// 与或（且非）
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="right">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool AndNot(this bool @this, bool right)
        {
            return @this && !right;
        }

        /// <summary>
        ///  与或（且非）
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="action">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool AndNot(this bool @this, Func<bool> action)
        {
            return @this && !action();
        }

        /// <summary>
        /// 或
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="right">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Or(this bool @this, bool right)
        {
            return @this || right;
        }

        /// <summary>
        /// 或
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="action">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Or(this bool @this, Func<bool> action)
        {
            return @this || action();
        }

        /// <summary>
        /// 或非
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="right">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool OrNot(this bool @this, bool right)
        {
            return @this || !right;
        }

        /// <summary>
        /// 或非
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="action">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool OrNot(this bool @this, Func<bool> action)
        {
            return @this || !action();
        }

        /// <summary>
        /// 亦或 
        /// true.Xor(false)=true
        /// false.Xor(false)=false
        /// true.Xor(true)=false
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="right">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Xor(this bool @this, bool right)
        {
            return @this ^ right;
        }

        /// <summary>
        /// 异或，只有一个为真时为真
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="action">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Xor(this bool @this, Func<bool> action)
        {
            return @this ^ action();
        }
    }
}