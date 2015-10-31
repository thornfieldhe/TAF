// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderByAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   排序特性
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    using System;

    /// <summary>
    /// 排序
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class OrderByAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderByAttribute"/> class. 
        /// 初始化排序
        /// </summary>
        /// <param name="sortId">
        /// 排序号
        /// </param>
        public OrderByAttribute(int sortId)
        {
            this.SortId = sortId;
        }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortId { get; set; }
    }
}