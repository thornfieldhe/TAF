// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheKeys.cs" company="" author="何翔华">
//   保存缓存键值对象
// </copyright>
// <summary>
//   CacheKeys
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.AccountModel
{
    using TAF.Utility;

    /// <summary>
    /// 存储缓存中的键
    /// </summary>
    public class CacheKeys : SingletonBase<CacheKeys>
    {
        public string Account { get; } = "Account_";

    }
}