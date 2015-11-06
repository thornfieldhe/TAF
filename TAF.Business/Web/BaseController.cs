﻿// --------------------------------------------------------------------------------------------------------------------
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
    using System.Web.Mvc;

    using AutoMapper;


    using TAF;
    using TAF.Core;

    /// <summary>
    /// The base controller.
    /// </summary>
    /// <typeparam name="K">
    /// </typeparam>
    /// <typeparam name="T">
    /// 对象列表视图
    /// </typeparam>
    /// <typeparam name="L">
    /// 对象视图
    /// </typeparam>
    [Authorize]
    public class BaseController<K, T, L> : Controller
        where K : EfBusiness<K>, new() where T : IEntityBase, new() where L : new()
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public virtual ActionResult Index()
        {
            return PartialView("_Index");
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public virtual ActionResult Get(Guid id)
        {
            var item = EfBusiness<K>.Get(id);
            return this.Json(new ActionResultData<K>(item), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The get view.
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
            var result = Mapper.Map<T>(item);
            return this.Json(new ActionResultData<T>(result), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public virtual ActionResult GetAll()
        {
            var items = EfBusiness<K>.GetAll();
            return this.Json(new ActionResultData<List<K>>(items), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The get views.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public virtual ActionResult GetViews()
        {
            var items = EfBusiness<K>.GetAll();
            var result = Mapper.Map<List<L>>(items);
            return this.Json(new ActionResultData<List<L>>(result), JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Edit(Guid? id)
        {
            return PartialView("_Edit", id.HasValue ? EfBusiness<K>.Get(id.Value) : new K());
        }

        /// <summary>
        /// The insert.
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
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// The update.
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
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// The delete.
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
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// The pager.
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
        protected ActionResult Pager<R>(int pageIndex, int pageSize, Func<K, bool> where, Func<K, R> orderBy, bool isAsc) where R : struct
        {
            var pager = EfBusiness<K>.Pages<R, L>(new Pager<L> { PageIndex = pageIndex, PageSize = pageSize }, where, orderBy, isAsc);
            return this.Json(pager, JsonRequestBehavior.AllowGet);
        }
    }
}