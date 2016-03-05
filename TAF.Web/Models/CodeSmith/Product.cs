namespace TAF.Web.Models
{

    using TAF;
    using TAF.Utility;

    /// <summary>
    /// 商品
    /// </summary>
    public partial class Product : EfBusiness<Product>
    {
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Code:" + Code.ToStr());
            AddDescription("Name:" + Name.ToStr());
            AddDescription("CategoryId:" + CategoryId.ToStr());
            AddDescription("ProductionDate:" + ProductionDate.ToStr());
            AddDescription("Unit:" + Unit.ToStr());
            AddDescription("Price:" + Price.ToStr());
            AddDescription("ColorId:" + ColorId.ToStr());
        }
        #endregion
    }
}