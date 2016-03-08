using System;
using System.Web.Mvc;

namespace TAF.Web.Controllers
{
    using System.Linq;

    using TAF.Mvc;
    using TAF.Utility;
    using TAF.Web.Models;

    [Authorize]
    public class ProductController : BaseController<Product, ProductItemView, ProductListView>
    {
        public override ActionResult Index()
        {
            var model = SystemDictionary.GetAll().Select(r => new Tuple<Guid, string, string>(r.Id, r.Key, r.Value)).ToList();
            return PartialView("_Index", model);
        }

        public ActionResult GetList(ProductQueryView query, int pageIndex, int pageSize = 20)
        {
            Func<Product, bool> func =
                r => (string.IsNullOrWhiteSpace(query.Name) || !string.IsNullOrWhiteSpace(query.Name) && r.Name.ToStr().Contains(query.Name.ToStr()))
                     && (query.CategoryId == new Guid() || r.CategoryId == query.CategoryId)
                     && (!query.ColorId.HasValue || query.ColorId == new Guid() || r.ColorId == query.ColorId.Value)
                    && (query.Price == 0 || query.Price == r.Price)
                    && (query.ProductionDateFrom == new DateTime(1, 1, 1) || query.ProductionDateFrom <= r.ProductionDate)
                    && (query.ProductionDateTo == new DateTime(1, 1, 1) || query.ProductionDateTo >= r.ProductionDate);
            return base.Pager(pageIndex, pageSize, func, r => r.CreatedDate);
        }

        public ActionResult Save(ProductItemView item)
        {
            return this.Update(item);
        }

    }
}