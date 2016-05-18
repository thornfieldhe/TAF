// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product.cs" author="何翔华">
//   
// </copyright>
// <summary>
//   商品
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
            AddDescription(nameof(Name), Name.ToStr());
            AddDescription(nameof(CategoryId), CategoryId.ToStr());
            AddDescription(nameof(ColorId), ColorId.ToStr());
            AddDescription(nameof(Price), Price.ToStr());
            AddDescription(nameof(ProductionDate), ProductionDate.ToStr());
        }
        #endregion
    }
}



