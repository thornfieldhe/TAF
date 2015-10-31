// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.EnumExt.cs" company="">
//   
// </copyright>
// <summary>
//   枚举扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    /// <summary>
    /// The extensions.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 获取描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="instance">
        /// 枚举实例
        /// </param>
        /// <returns>
        /// </returns>
        public static string Description(this System.Enum instance)
        {
            return EnumExt.GetDescription(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="instance">
        /// 枚举实例
        /// </param>
        /// <returns>
        /// </returns>
        public static int Value(this System.Enum instance)
        {
            return EnumExt.GetValue(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <typeparam name="T">
        /// 返回值类型
        /// </typeparam>
        /// <param name="instance">
        /// 枚举实例
        /// </param>
        /// <returns>
        /// </returns>
        public static T Value<T>(this System.Enum instance)
        {
            return Value(instance).To<T>();
        }
    }
}