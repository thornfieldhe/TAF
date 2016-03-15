namespace TAF.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 文章
    /// </summary>
    public partial class Article
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get; set;
        }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishDate
        {
            get; set;
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get; set;
        }

        /// <summary>
        /// 文章分类Id
        /// </summary>
        public Guid CategoryId
        {
            get; set;
        }

        /// <summary>
        /// 文章分类
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual SystemDictionary Category
        {
            get; set;
        }

    }
}