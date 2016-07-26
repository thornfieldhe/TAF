namespace TAF.Business
{
    using System;
    using System.ComponentModel;

    [Flags]
    public enum InventoryAccess
    {
        /// <summary>
        /// 拒绝
        /// </summary>
        [Description("拒绝")]
        Forbid = 0,

        /// <summary>
        /// 读
        /// </summary>
        [Description("读")]
        Read = 1,

        /// <summary>
        /// 写
        /// </summary>
        [Description("写")]
        Write = 2,

        /// <summary>
        /// 执行
        /// </summary>
        [Description("执行")]
        Excute = 4
    }
}