namespace TAF.Utility
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Dictionary扩展
    /// </summary>
    public partial class Extensions
    {
        /// <summary>
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        /// <param name="dict">
        /// The dict.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// 返回更新后Dictionary列表
        /// </returns>
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key) == false)
            {
                dict.Add(key, value);
            }

            return dict;
        }

        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// </summary>
        /// <param name="dict">
        /// The dict.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// 返回更新后Dictionary列表
        /// </returns>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }

        /// <summary>
        /// 向字典中批量添加键值对
        /// 没有考虑线程安全的问题
        /// </summary>
        /// <param name="dict">
        /// The dict.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="replaceExisted">
        /// 如果已存在，是否替换
        /// </param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
        {
            foreach (var item in values.Where(item => dict.ContainsKey(item.Key) == false || replaceExisted))
            {
                dict[item.Key] = item.Value;
            }

            return dict;
        }
    }
}