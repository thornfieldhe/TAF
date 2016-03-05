using System;
using System.Web.Mvc;

namespace TAF.Web.Controllers
{
    using TAF.Mvc;
    using TAF.Utility;
    using TAF.Web.Models;

    public class DictionaryController : BaseController<SystemDictionary, SystemDictionary, SystemDictionaryView>
    {

        public ActionResult GetList(SystemDictionary query, int pageIndex, int pageSize = 20)
        {
            Func<SystemDictionary, bool> func =
                r => (string.IsNullOrWhiteSpace(query.Key)
                      || (!string.IsNullOrWhiteSpace(query.Key) && r.Key.ToStr() == query.Key.ToStr()))
                     && (string.IsNullOrWhiteSpace(query.Value)
                         || (!string.IsNullOrWhiteSpace(query.Value) && r.Value.ToStr() == query.Value.ToStr()))
                     && (string.IsNullOrWhiteSpace(query.Value1)
                         || (!string.IsNullOrWhiteSpace(query.Value1)
                             && r.Value1.ToStr() == query.Value1.ToStr()))
                     && (string.IsNullOrWhiteSpace(query.Value2)
                         || (!string.IsNullOrWhiteSpace(query.Value2)
                             && r.Value2.ToStr() == query.Value2.ToStr()))
                     && (string.IsNullOrWhiteSpace(query.Value3)
                         || (!string.IsNullOrWhiteSpace(query.Value3)
                             && r.Value3.ToStr() == query.Value3.ToStr()))
                     && (string.IsNullOrWhiteSpace(query.Value4)
                         || (!string.IsNullOrWhiteSpace(query.Value4)
                             && r.Value4.ToStr() == query.Value4.ToStr()))
                     && (string.IsNullOrWhiteSpace(query.Value5)
                         || (!string.IsNullOrWhiteSpace(query.Value5)
                             && r.Value5.ToStr() == query.Value5.ToStr()))
                     && (string.IsNullOrWhiteSpace(query.Value6)
                         || (!string.IsNullOrWhiteSpace(query.Value6)
                             && r.Value6.ToStr() == query.Value6.ToStr()))
                     && (string.IsNullOrWhiteSpace(query.Value7)
                         || (!string.IsNullOrWhiteSpace(query.Value7)
                             && r.Value7.ToStr() == query.Value7.ToStr()))
                     && (string.IsNullOrWhiteSpace(query.Value8)
                         || (!string.IsNullOrWhiteSpace(query.Value8)
                             && r.Value8.ToStr() == query.Value8.ToStr()))
                     && (string.IsNullOrWhiteSpace(query.Value9)
                         || (!string.IsNullOrWhiteSpace(query.Value9)
                             && r.Value9.ToStr() == query.Value9.ToStr()));
            return base.Pager(pageIndex, pageSize, func, r => r.Value);
        }

    }
}