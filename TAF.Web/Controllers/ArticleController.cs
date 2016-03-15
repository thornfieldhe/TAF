// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArticleController.cs" author="何翔华">
//   
// </copyright>
// <summary>
//   文章
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Web.Mvc;

namespace TAF.Web.Controllers
{

    using TAF.Mvc;
    using TAF.Utility;
    using TAF.Web.Models;

    /// <summary>
    /// 文章
    /// </summary>
    [Authorize]
    public class ArticleController : BaseController<Article, ArticleItemView, ArticleListView>
    {
        public override ActionResult Index()
        {
            var model = SystemDictionary.GetAll().Select(r => new Tuple<Guid, string, string>(r.Id, r.Key, r.Value)).ToList();
            return PartialView("_Index", model);
        }
    
        public ActionResult GetList(ArticleQueryView query, int pageIndex, int pageSize = 20)
        {
            Func<Article,bool> func = r => 
             (string.IsNullOrWhiteSpace(query.Title)||(!string.IsNullOrWhiteSpace(query.Title) && r.Title.Contains(query.Title.ToStr())))
             && ((query.PublishDateFrom==query.PublishDateTo && query.PublishDateTo== DateTime.Today)
             || (query.PublishDateFrom==new DateTime(1, 1, 1) || query.PublishDateFrom<= r.PublishDate)
             && (query.PublishDateTo==new DateTime(1, 1, 1) || query.PublishDateTo>= r.PublishDate))
             && (string.IsNullOrWhiteSpace(query.Content)||(!string.IsNullOrWhiteSpace(query.Content) && r.Content.Contains(query.Content.ToStr())))
             && (query.CategoryId==new Guid()||query.CategoryId== r.CategoryId);

             return base.Pager(pageIndex, pageSize, func, r => r.CreatedDate);
        }
        
        public ActionResult Save(ArticleItemView item)
        {
            return this.Update(item);
        }
    }
}



