// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductView.cs" author="何翔华">
//   
// </copyright>
// <summary>
//   商品
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;
namespace TAF.Web.Models
{
    using TAF.Core;

    /// <summary>
    /// 商品对象视图
    /// </summary>
    public class ProductItemView : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public Guid CategoryId
        {
            get; set;
        }

        public Guid? ColorId
        {
            get; set;
        }

        public decimal Price
        {
            get; set;
        }

        public string ProductionDate
        {
            get; set;
        }

    }

    /// <summary>
    /// 商品列表视图
    /// </summary>
    public class ProductListView : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
        public string Category
        {
            get; set;
        }
        public string Color
        {
            get; set;
        }
        public decimal Price
        {
            get; set;
        }
        public string ProductionDate
        {
            get; set;
        }
    }

    /// <summary>
    /// 商品查询视图
    /// </summary>
    public class ProductQueryView
    {

        public string Name
        {
            get; set;
        }

        public Guid CategoryId
        {
            get; set;
        }

        public Guid? ColorId
        {
            get; set;
        }

        public decimal Price
        {
            get; set;
        }

        public DateTime ProductionDateFrom
        {
            get; set;
        }

        public DateTime ProductionDateTo
        {
            get; set;
        }
    }
}









