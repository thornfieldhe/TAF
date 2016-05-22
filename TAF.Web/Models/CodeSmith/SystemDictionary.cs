namespace TAF.Web.Models
{

    using TAF;
    using TAF.Utility;

    /// <summary>
    /// 商品
    /// </summary>
    public partial class SystemDictionary : BaseBusiness<SystemDictionary>
    {
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription(nameof(Key), Key.ToStr());
            AddDescription(nameof(Value), Value.ToStr());
            AddDescription(nameof(Value1), Value1.ToStr());
            AddDescription(nameof(Value2), Value2.ToStr());
            AddDescription(nameof(Value3), Value3.ToStr());
            AddDescription(nameof(Value4), Value4.ToStr());
            AddDescription(nameof(Value5), Value5.ToStr());
            AddDescription(nameof(Value6), Value6.ToStr());
            AddDescription(nameof(Value7), Value7.ToStr());
            AddDescription(nameof(Value8), Value8.ToStr());
            AddDescription(nameof(Value9), Value9.ToStr());
        }
        #endregion
    }
}