// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationResultCollection.cs" company="">
//   
// </copyright>
// <summary>
//   验证结果集合
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Validation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 验证结果集合
    /// </summary>
    [Serializable]
    public class ValidationResultCollection : IEnumerable<ValidationResult>
    {
        /// <summary>
        /// 验证结果
        /// </summary>
        private readonly List<ValidationResult> results;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResultCollection"/> class. 
        /// 初始化验证结果集合
        /// </summary>
        public ValidationResultCollection()
        {
            results = new List<ValidationResult>();
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get
            {
                return results.Count == 0;
            }
        }

        /// <summary>
        /// 验证结果个数
        /// </summary>
        public int Count
        {
            get
            {
                return results.Count;
            }
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator<ValidationResult> IEnumerable<ValidationResult>.GetEnumerator()
        {
            return results.GetEnumerator();
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return results.GetEnumerator();
        }

        /// <summary>
        /// 添加验证结果
        /// </summary>
        /// <param name="result">
        /// 验证结果
        /// </param>
        public void Add(ValidationResult result)
        {
            if (result == null)
            {
                return;
            }

            results.Add(result);
        }

        /// <summary>
        /// 添加验证结果集合
        /// </summary>
        /// <param name="results">
        /// 验证结果集合
        /// </param>
        public void AddResults(IEnumerable<ValidationResult> results)
        {
            if (results == null)
            {
                return;
            }

            foreach (var result in results)
            {
                Add(result);
            }
        }
    }
}