// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArticleView.cs" author="何翔华">
//   
// </copyright>
// <summary>
//   文章
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;
namespace TAF.Web.Models
{
    using TAF.Core;

    /// <summary>
    /// 文章对象视图
    /// </summary>
    public class ArticleItemView : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }
        
        public string Title
        {
            get; set;
        }
        
        public string PublishDate
        {
            get; set;
        }
        
        public string Content
        {
            get; set;
        }
        
        public Guid CategoryId
        {
            get; set;
        }
        
    }

    /// <summary>
    /// 文章列表视图
    /// </summary>
    public class ArticleListView : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }
        
        public string Title
        {
            get; set;
        }
        public string PublishDate
        {
            get; set;
        }
        public string Content
        {
            get; set;
        }
        public string Category
        {
            get; set;
        }
    }
    
    /// <summary>
    /// 文章查询视图
    /// </summary>
    public class ArticleQueryView 
    {        
        
        public string Title
        {
            get; set;
        }        
        
        public DateTime PublishDateFrom
        {
            get; set;
        }  
        
        public DateTime PublishDateTo
        {
            get; set;
        }  
        
        public string Content
        {
            get; set;
        }        
        
        public Guid CategoryId
        {
            get; set;
        }        
    }
}





