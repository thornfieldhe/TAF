namespace TAF.Business
{
    using System;

    /// <summary>
    /// The action result data.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ActionResultData<T> : ActionResultStatus where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultData{T}"/> class. 
        /// </summary>
        /// <param name="authority">
        /// 权限
        /// </param>
        /// <param name="result">
        /// 结果
        /// </param>
        public ActionResultData(int authority, T result = null) : base(authority, null)
        {
            Data = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultStatus"/> class. 
        /// 带错误信息初始化函数
        /// </summary>
        /// <param name="authority"></param>
        /// <param name="errorCode">
        /// 错误消息代码
        /// </param>
        /// <param name="errorMessage">
        /// 错误消息
        /// </param>>
        public ActionResultData(int authority, int errorCode = 500, string errorMessage = "") : base(authority, errorCode, errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultStatus"/> class. 
        /// 异常初始化函数
        /// </summary>
        /// <param name="authority">
        /// </param>
        /// <param name="ex">
        /// 异常
        /// </param>
        public ActionResultData(int authority, Exception ex = null) : base(authority, ex)
        {
        }

        /// <summary>
        /// 返回值
        /// </summary>
        public T Data
        {
            get; private set;
        }
    }
}