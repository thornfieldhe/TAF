namespace TAF.Utility
{
    /// <summary>
    /// 单例基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonBase<T> where T : new()
    {
        protected SingletonBase()
        {
        }

        /// <summary>
        /// 默认实例
        /// </summary>
        public static readonly T Instance = new T();
    }
}