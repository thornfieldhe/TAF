// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Log.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   Log
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Log
{
    using TAF.Utility;

    /// <summary>
    /// 操作日志
    /// </summary>
    public class Log : BaseBusiness<Log>
    {
        /// <summary>
        /// 操作
        /// </summary>
        public string Action
        {
            get; set;
        }
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription(nameof(Action), Action.ToStr());
        }
        #endregion
    }
}