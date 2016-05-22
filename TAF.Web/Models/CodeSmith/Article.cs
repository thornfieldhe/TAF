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
    public partial class Article : BaseBusiness<Article>
    {
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription(nameof(Title), Title.ToStr());
            AddDescription(nameof(PublishDate), PublishDate.ToStr());
            AddDescription(nameof(Content), Content.ToStr());
            AddDescription(nameof(CategoryId), CategoryId.ToStr());
        }
        #endregion
    }
}



