namespace TAF.Web.Models
{

    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 商品
    /// </summary>
    [Table("Products")]
    public partial class Product
    {
        #region 属性

        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        [Description("商品名称")]
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 商品类别Id
        /// </summary>
        [Description("商品类别Id")]
        public Guid CategoryId
        {
            get; set;
        }

        [ForeignKey("CategoryId")]
        [Description("商品类别")]
        public virtual SystemDictionary Category
        {
            get; set;
        }

        /// <summary>
        /// 颜色Id
        /// </summary>
        [Description("颜色Id")]
        public Guid? ColorId
        {
            get; set;
        }

        [ForeignKey("ColorId")]
        [Description("颜色")]
        public virtual SystemDictionary Color
        {
            get; set;
        }

        /// <summary>
        /// 价格
        /// </summary>
        [Required]
        [Min]
        [Description("价格")]
        public decimal Price
        {
            get; set;
        }

        /// <summary>
        /// 生产日期
        /// </summary>
        [Required]
        [Description("生产日期")]
        public DateTime ProductionDate
        {
            get; set;
        }

        #endregion

    }
}