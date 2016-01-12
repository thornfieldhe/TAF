// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Comparison.cs" company="">
//   
// </copyright>
// <summary>
//   比较器辅助类，用于快速创建IComparer{T}接口的实例
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 比较器辅助类，用于快速创建IComparer{T}接口的实例
    /// </summary>
    /// <example>
    /// </example>
    /// <typeparam name="T">要比较的类型</typeparam>
    public static class Comparison<T>
    {
        /// <summary>
        /// 创建指定对比委托<paramref name="keySelector"/>的实例
        /// </summary>
        /// <typeparam name="TV">
        /// </typeparam>
        /// <param name="keySelector">
        /// The key Selector.
        /// </param>
        /// <returns>
        /// 继承IComparer接口的比较类
        /// </returns>
        public static IComparer<T> CreateComparer<TV>(Func<T, TV> keySelector)
        {
            return new CommonComparer<TV>(keySelector);
        }

        /// <summary>
        /// 创建指定对比委托<paramref name="keySelector"/>与结果二次比较器<paramref name="comparer"/>的实例
        /// </summary>
        /// <typeparam name="TV">
        /// </typeparam>
        /// <param name="keySelector">
        /// The key Selector.
        /// </param>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        /// <returns>
        /// IComparer{T}对象
        /// </returns>
        public static IComparer<T> CreateComparer<TV>(Func<T, TV> keySelector, IComparer<TV> comparer)
        {
            return new CommonComparer<TV>(keySelector, comparer);
        }

        #region Nested type: CommonComparer

        private class CommonComparer<TV> : IComparer<T>
        {
            private readonly IComparer<TV> _comparer;
            private readonly Func<T, TV> _keySelector;

            public CommonComparer(Func<T, TV> keySelector, IComparer<TV> comparer)
            {
                _keySelector = keySelector;
                _comparer = comparer;
            }

            public CommonComparer(Func<T, TV> keySelector)
                : this(keySelector, Comparer<TV>.Default)
            {
            }

            #region IComparer<T> Members

            public int Compare(T x, T y)
            {
                return _comparer.Compare(_keySelector(x), _keySelector(y));
            }

            #endregion
        }

        #endregion
    }
}