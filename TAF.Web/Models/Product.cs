namespace TAF.Web.Models
{

    using System;
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
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 商品类别Id
        /// </summary>
        public Guid CategoryId
        {
            get; set;
        }

        [ForeignKey("CategoryId")]
        public virtual SystemDictionary Category
        {
            get; set;
        }

        /// <summary>
        /// 颜色Id
        /// </summary>
        public Guid? ColorId
        {
            get; set;
        }

        [ForeignKey("ColorId")]
        public virtual SystemDictionary Color
        {
            get; set;
        }

        /// <summary>
        /// 价格
        /// </summary>
        [Required]
        [Min]
        public decimal Price
        {
            get; set;
        }

        /// <summary>
        /// 生产日期
        /// </summary>
        [Required]
        public DateTime ProductionDate
        {
            get; set;
        }

        #endregion

    }
}