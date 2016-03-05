namespace TAF.Web.Models
{

    using TAF;
    using TAF.Utility;

    /// <summary>
    /// 商品
    /// </summary>
    public partial class SystemDictionary : EfBusiness<SystemDictionary>
    {
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Key:" + Key.ToStr());
            AddDescription("Value:" + Value.ToStr());
            AddDescription("Value1:" + Value1.ToStr());
            AddDescription("Value2:" + Value2.ToStr());
            AddDescription("Value3:" + Value3.ToStr());
            AddDescription("Value4:" + Value4.ToStr());
            AddDescription("Value5:" + Value5.ToStr());
            AddDescription("Value6:" + Value6.ToStr());
            AddDescription("Value7:" + Value7.ToStr());
            AddDescription("Value8:" + Value8.ToStr());
            AddDescription("Value9:" + Value9.ToStr());
        }
        #endregion
    }
}