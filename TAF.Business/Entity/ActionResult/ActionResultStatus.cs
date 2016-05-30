namespace TAF.Business
{
    using System;

    /// <summary>
    /// 无返回值封装类
    /// </summary>
    public class ActionResultStatus
    {
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
        public ActionResultStatus(int authority, int errorCode = 500, string errorMessage = "")
        {
            if (authority == 0)
            {
                this.ErrorCode = 401;
                this.Message = "401";
            }
            else
            {
                Message = errorMessage;
                ErrorCode = errorCode;
            }

            Status = ActionStatuses.Error;
            Authority = authority;
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
        public ActionResultStatus(int authority, Exception ex)
        {
            if (authority == 0)
            {
                this.ErrorCode = 401;
                this.Message = "401";
                this.Status = ActionStatuses.Error;
            }
            else if (ex != null)
            {
                Status = ActionStatuses.Error;
                Message = ex.Message;
                ErrorCode = 500;
            }
            else
            {
                this.Status = ActionStatuses.OK;
            }

            Authority = authority;
        }

        /// <summary>
        /// 执行状态
        /// </summary>
        public ActionStatuses Status
        {
            get; protected set;
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message
        {
            get; protected set;
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode
        {
            get; protected set;
        }

        /// <summary>
        /// 权限：0：未授权，1读，2写，4执行
        /// </summary>
        public int Authority
        {
            get; protected set;
        }
    }
}