// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="">
//   
// </copyright>
// <summary>
//   The base controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;


    using TAF;
    using TAF.Core;
    using TAF.Utility;

    /// <summary>
    /// 控制层基类
    /// </summary>
    /// <typeparam name="K">
    /// 实体对象
    /// </typeparam>
    /// <typeparam name="T">
    /// 对象列表视图
    /// </typeparam>
    /// <typeparam name="L">
    /// 对象视图
    /// </typeparam>
    [Authorize]
    public class BaseController<K, T, L> : BaseTAFController
        where K : EfBusiness<K>, new() where T : class, IEntityBase, new() where L : new()
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public virtual ActionResult Index()
        {
            return PartialView("_Index");
        }

        /// <summary>
        /// 获取单条数据视图
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public virtual ActionResult GetView(Guid id)
        {
            var item = EfBusiness<K>.Get(id);
            if (item is T)
            {
                return this.Json(new ActionResultData<T>(item as T), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new ActionResultData<T>(Mapper.Map<T>(item)), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取所有数据视图
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public virtual ActionResult GetViews()
        {
            var items = EfBusiness<K>.GetAll();
            if (items is List<L>)
            {
                return this.Json(new ActionResultData<List<L>>(items as List<L>), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new ActionResultData<List<L>>(Mapper.Map<List<L>>(items)), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 编辑页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ActionResult Edit(Guid? id)
        {
            return this.PartialView("_Edit", !id.HasValue ? new K() : EfBusiness<K>.Get(id.Value));
        }

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public virtual ActionResult Insert(T value)
        {
            try
            {
                var item = Mapper.Map<K>(value);
                item.Create();
                return this.Json(new ActionResultStatus());
            }
            catch (DbEntityValidationException ex)
            {
                return this.Json(new ActionResultStatus(100,
                    $"{ex.EntityValidationErrors.First().Entry.ToStr()}:{ex.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage}"));
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex));
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public virtual ActionResult Update(T value)
        {
            try
            {
                var item = EfBusiness<K>.Get(value.Id);
                Mapper.Map(value, item);
                item.Save();
                return this.Json(new ActionResultStatus());
            }
            catch (DbEntityValidationException ex)
            {
                return this.Json(new ActionResultStatus(100,
                    $"{ex.EntityValidationErrors.First().Entry.ToStr()}:{ex.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage}"));
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex));
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public virtual ActionResult Delete(Guid id)
        {
            try
            {
                var item = EfBusiness<K>.Get(id);
                item.Delete();
                return this.Json(new ActionResultStatus());
            }
            catch (DbEntityValidationException ex)
            {
                return this.Json(new ActionResultStatus(100,
                    $"{ex.EntityValidationErrors.First().Entry.ToStr()}:{ex.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage}"));
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex));
            }
        }

        /// <summary>
        /// 分页获取数据视图
        /// </summary>
        /// <typeparam name="R">
        /// </typeparam>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="where">
        /// The where.
        /// </param>
        /// <param name="orderBy">
        /// The order By.
        /// </param>
        /// <param name="isAsc"></param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Pager<R>(int pageIndex, int pageSize, Func<K, bool> where, Func<K, R> orderBy, bool isAsc) where R : struct
        {
            var pager = EfBusiness<K>.Pages<R, L>(new Pager<L> { PageIndex = pageIndex, PageSize = pageSize }, where, orderBy, isAsc);
            return this.Json(pager, JsonRequestBehavior.AllowGet);
        }
    }
}