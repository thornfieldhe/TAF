// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Article.cs" author="何翔华">
//   
// </copyright>
// <summary>
//   文章
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Web.Models
{
    using TAF;
    using TAF.Utility;

    /// <summary>
    /// 文章
    /// </summary>
    public partial class Article : EfBusiness<Article>
    {
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Title:" + Title.ToStr());
            AddDescription("PublishDate:" + PublishDate.ToStr());
            AddDescription("Content:" + Content.ToStr());
            AddDescription("CategoryId:" + CategoryId.ToStr());
        }
        #endregion
    }
}



