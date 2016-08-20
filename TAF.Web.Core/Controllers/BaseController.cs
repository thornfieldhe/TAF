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
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using CacheManager.Core;

    using TAF;
    using TAF.Business;
    using TAF.Business.Entity;
    using TAF.Mvc.View;

    /// <summary>
    /// 控制层基类
    /// </summary>
    [Authorize]
    public class BaseController : ApiController
    {
        private string token;
        private AuthorisedUserView userInfo;

        public string Token
        {
            get
            {
                if (this.token == null)
                {
                    var authorizationToken = this.ActionContext.Request.Headers.SingleOrDefault(r => r.Key == "token");
                    if (authorizationToken.Value != null
                 && Ioc.Create<ICacheManager<object>>().Get(authorizationToken.Value.First()) != null)
                    {
                        this.token = (authorizationToken.Value.First());
                    }
                }
                return this.token;
            }
        }

        /// <summary>
        /// 用户基本信息：用户名，角色
        /// </summary>
        public AuthorisedUserView UserInfo
        {
            get
            {
                if (this.userInfo == null)
                {
                    if (this.Token == null)
                    {
                        return null;
                    }
                    this.userInfo = Ioc.Create<ICacheManager<object>>().Get(this.Token) as AuthorisedUserView;
                }
                return this.userInfo;
            }
        }

        public Guid UserId
        {
            get
            {
                if (this.UserInfo == null)
                {
                    return Guid.Empty;
                }
                return this.UserInfo.Id;
            }
        }

        /// <summary>
        /// 判断用户是否有权限执行操作并返回值
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="R">
        /// </typeparam>
        /// <param name="id">
        /// </param>
        /// <param name="writeFunc">
        /// 返回带权限的结果数据
        /// </param>
        /// <param name="readFunc">
        /// </param>
        /// <param name="rEqualw">
        /// 为是否允许read执行write操作
        /// </param>
        /// <returns>
        /// </returns>
        protected ActionResultData<R> AuthorizationWithData<T, R>(Guid id, Func<R> writeFunc = null, Func<R> readFunc = null, bool rEqualw = true) where T : PermissionBase<T>, new() where R : class
        {
            if ((new T().GetPermissionFromCache(id, this.UserId) & (int)InventoryAccess.Write) > 0)
            {
                return writeFunc == null ? new ActionResultData<R>((int)InventoryAccess.Forbid, errorMessage: "方法不存在") : new ActionResultData<R>((int)InventoryAccess.Write, writeFunc());
            }
            else if ((new T().GetPermissionFromCache(id, this.UserId) & (int)InventoryAccess.Read) > 0)
            {
                if (rEqualw)
                {
                    return writeFunc == null ? new ActionResultData<R>((int)InventoryAccess.Forbid, errorMessage: "方法不存在") : new ActionResultData<R>((int)InventoryAccess.Write, writeFunc());
                }
                else
                {
                    return readFunc == null ? new ActionResultData<R>((int)InventoryAccess.Forbid, errorMessage: "方法不存在") : new ActionResultData<R>((int)InventoryAccess.Read, readFunc());
                }
            }

            return new ActionResultData<R>((int)InventoryAccess.Forbid, errorMessage: "用户未授权");
        }

        /// <summary>
        /// 异步判断用户是否有权限执行操作并返回值
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="R">
        /// </typeparam>
        /// <param name="id">
        /// </param>
        /// <param name="writeFunc">
        /// 返回带权限的结果数据
        /// </param>
        /// <param name="readFunc">
        /// </param>
        /// <param name="rEqualw">
        /// 为是否允许read执行write操作
        /// </param>
        /// <returns>
        /// </returns>
        protected async Task<ActionResultData<R>> AuthorizationWithDataAsync<T, R>(Guid id, Func<Task<R>> writeFunc = null, Func<Task<R>> readFunc = null, bool rEqualw = true) where T : PermissionBase<T>, new() where R : class
        {
            if ((new T().GetPermissionFromCache(id, this.UserId) & (int)InventoryAccess.Write) > 0)
            {
                return writeFunc == null ? new ActionResultData<R>((int)InventoryAccess.Forbid, errorMessage: "方法不存在") : new ActionResultData<R>((int)InventoryAccess.Write, await writeFunc());
            }
            else if ((new T().GetPermissionFromCache(id, this.UserId) & (int)InventoryAccess.Read) > 0)
            {
                if (rEqualw)
                {
                    return writeFunc == null ? new ActionResultData<R>((int)InventoryAccess.Forbid, errorMessage: "方法不存在") : new ActionResultData<R>((int)InventoryAccess.Read, await writeFunc());
                }
                else
                {
                    return readFunc == null ? new ActionResultData<R>((int)InventoryAccess.Forbid, errorMessage: "方法不存在") : new ActionResultData<R>((int)InventoryAccess.Read, await readFunc());
                }
            }

            return new ActionResultData<R>((int)InventoryAccess.Forbid, errorMessage: "用户未授权");
        }

        /// <summary>
        /// 判断用户是否有权限执行操作
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="id">
        /// </param>
        /// <param name="writeFunc">
        /// 写操作
        /// </param>
        /// <param name="readFunc">
        /// 读操作
        /// </param>
        /// <param name="rEqualw">
        /// 为是否允许read执行write操作
        /// </param>
        /// <returns>
        /// </returns>
        protected ActionResultStatus AuthorizationStatus<T>(Guid id, Action writeFunc = null, Action readFunc = null, bool rEqualw = true) where T : PermissionBase<T>, new()
        {
            if ((new T().GetPermissionFromCache(id, this.UserId) & (int)InventoryAccess.Write) > 0)
            {
                if (writeFunc == null)
                {
                    return new ActionResultStatus((int)InventoryAccess.Forbid, errorMessage: "方法不存在");
                }

                writeFunc();
                return new ActionResultStatus((int)InventoryAccess.Write);
            }
            else if ((new T().GetPermissionFromCache(id, this.UserId) & (int)InventoryAccess.Read) > 0)
            {
                if (rEqualw)
                {
                    if (writeFunc == null)
                    {
                        return new ActionResultStatus((int)InventoryAccess.Forbid, errorMessage: "方法不存在");
                    }

                    writeFunc();
                    return new ActionResultStatus((int)InventoryAccess.Read);
                }
                else
                {
                    if (readFunc == null)
                    {
                        return new ActionResultStatus((int)InventoryAccess.Forbid, errorMessage: "方法不存在");
                    }

                    readFunc();
                    return new ActionResultStatus((int)InventoryAccess.Read);
                }
            }

            return new ActionResultStatus((int)InventoryAccess.Forbid, errorMessage: "用户未授权");
        }

        /// <summary>
        /// 异步判断用户是否有权限执行操作
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="id">
        /// </param>
        /// <param name="writeFunc">
        /// 写操作
        /// </param>
        /// <param name="readFunc">
        /// 读操作
        /// </param>
        /// <param name="rEqualw">
        /// 为是否允许read执行write操作
        /// </param>
        /// <returns>
        /// </returns>
        protected async Task<ActionResultStatus> AuthorizationStatusAsync<T>(Guid id, Func<Task> writeFunc = null, Func<Task> readFunc = null, bool rEqualw = true) where T : PermissionBase<T>, new()
        {
            if ((new T().GetPermissionFromCache(id, this.UserId) & (int)InventoryAccess.Write) > 0)
            {
                if (writeFunc == null)
                {
                    return new ActionResultStatus((int)InventoryAccess.Forbid, errorMessage: "方法不存在");
                }

                await writeFunc();
                return new ActionResultStatus((int)InventoryAccess.Write);
            }
            else if ((new T().GetPermissionFromCache(id, this.UserId) & (int)InventoryAccess.Read) > 0)
            {
                if (rEqualw)
                {
                    if (writeFunc == null)
                    {
                        return new ActionResultStatus((int)InventoryAccess.Forbid, errorMessage: "方法不存在");
                    }

                    await writeFunc();
                    return new ActionResultStatus((int)InventoryAccess.Read);
                }
                else
                {
                    if (readFunc == null)
                    {
                        return new ActionResultStatus((int)InventoryAccess.Forbid, errorMessage: "方法不存在");
                    }

                    await readFunc();
                    return new ActionResultStatus((int)InventoryAccess.Read);
                }
            }

            return new ActionResultStatus((int)InventoryAccess.Forbid, errorMessage: "用户未授权");
        }

        #region 注释

        //
        //        /// <summary>
        //        /// 获取单条数据视图
        //        /// </summary>
        //        /// <param name="id">
        //        /// The id.
        //        /// </param>
        //        /// <returns>
        //        /// The <see cref="System.Web.Mvc.ActionResult"/>.
        //        /// </returns>
        //        public virtual ActionResultData<K> Get(Guid id)
        //        {
        //            var item = BaseBusiness<K>.Find(id, true);
        //            return new ActionResultData<K>(0, item);
        //        }
        //
        //        /// <summary>
        //        /// 获取单条数据视图
        //        /// </summary>
        //        /// <typeparam name="T">
        //        /// </typeparam>
        //        /// <param name="id">
        //        /// The id.
        //        /// </param>
        //        /// <returns>
        //        /// The <see cref="System.Web.Mvc.ActionResult"/>.
        //        /// </returns>
        //        public virtual ActionResultData<T> GetView<T>(Guid id) where T : class, IEntityBase
        //        {
        //            var item = BaseBusiness<K>.Find<T>(id, true);
        //            return new ActionResultData<T>(0, item);
        //        }
        //
        //        /// <summary>
        //        /// 获取所有数据
        //        /// </summary>
        //        /// <returns>
        //        /// The <see cref="System.Web.Mvc.ActionResult"/>.
        //        /// </returns>
        //        public ActionResultData<List<K>> GetAll()
        //        {
        //            var items = BaseBusiness<K>.GetAll();
        //            return new ActionResultData<List<K>>(0, items);
        //        }
        //
        //        /// <summary>
        //        /// 获取所有数据视图
        //        /// </summary>
        //        /// <typeparam name="T">
        //        /// </typeparam>
        //        /// <returns>
        //        /// The <see cref="System.Web.Mvc.ActionResult"/>.
        //        /// </returns>
        //        public ActionResultData<List<T>> GetAllViews<T>() where T : class, IEntityBase
        //        {
        //            var items = BaseBusiness<K>.GetAll<T>();
        //            return new ActionResultData<List<T>>(0, items);
        //        }
        //
        //        /// <summary>
        //        /// 保存一条数据
        //        /// </summary>
        //        /// <param name="value">
        //        /// The value.
        //        /// </param>
        //        /// <returns>
        //        /// The <see cref="ActionResultData"/>.
        //        /// </returns>
        //        protected ActionResultData<bool> Save(T value)
        //        {
        //            var item = BaseBusiness<K>.Find(value.Id);
        //            if (item == null)
        //            {
        //                item = Mapper.Map<K>(value);
        //                return new ActionResultData<bool>(0, item.Create(this.UserId) == 1);
        //            }
        //            else
        //            {
        //                Mapper.Map(value, item);
        //                return new ActionResultData<bool>(0, item.Save(this.UserId) == 1);
        //            }
        //        }
        //
        //        /// <summary>
        //        /// 删除一条数据
        //        /// </summary>
        //        /// <param name="id">
        //        /// The id.
        //        /// </param>
        //        /// <returns>
        //        /// The <see cref="ActionResultData"/>.
        //        /// </returns>
        //        [HttpPost]
        //        public ActionResultData<bool> Delete(Guid id)
        //        {
        //            var result = BaseBusiness<K>.Delete(r => r.Id == id) > 0;
        //            return new ActionResultData<bool>(1, result);
        //        }
        //
        //        /// <summary>
        //        /// 分页获取数据
        //        /// </summary>
        //        /// <typeparam name="R">
        //        /// ListView
        //        /// </typeparam>
        //        /// <param name="pageIndex">
        //        /// The page index.
        //        /// </param>
        //        /// <param name="pageSize">
        //        /// The page size.
        //        /// </param>
        //        /// <param name="where">
        //        /// The where.
        //        /// </param>
        //        /// <param name="orderBy">
        //        /// The order By.
        //        /// </param>
        //        /// <param name="isAsc"></param>
        //        /// <returns>
        //        /// The <see cref="ActionResultData"/>.
        //        /// </returns>
        //        protected virtual ActionResultData<Pager<K>> Pager<R>(int pageIndex, int pageSize, Func<K, bool> where, Func<K, R> orderBy, bool isAsc = true)
        //        {
        //            var pager = BaseBusiness<K>.Pages(new Pager<K> { PageIndex = pageIndex, PageSize = pageSize }, where, orderBy, isAsc);
        //            return new ActionResultData<Pager<K>>(1, pager);
        //        }
        //
        //        /// <summary>
        //        /// 分页获取数据视图
        //        /// </summary>
        //        /// <typeparam name="R">
        //        /// ListView
        //        /// </typeparam>
        //        /// <typeparam name="T">
        //        /// </typeparam>
        //        /// <param name="pageIndex">
        //        /// The page index.
        //        /// </param>
        //        /// <param name="pageSize">
        //        /// The page size.
        //        /// </param>
        //        /// <param name="where">
        //        /// The where.
        //        /// </param>
        //        /// <param name="orderBy">
        //        /// The order By.
        //        /// </param>
        //        /// <param name="isAsc">
        //        /// </param>
        //        /// <returns>
        //        /// The <see cref="ActionResultData"/>.
        //        /// </returns>
        //        protected virtual ActionResultData<Pager<T>> PagerView<R, T>(
        //            int pageIndex,
        //            int pageSize,
        //            Func<K, bool> where,
        //            Func<K, R> orderBy,
        //            bool isAsc = true) where T : BaseBusiness<T>, new()
        //        {
        //            var pager = BaseBusiness<K>.Pages<R, K, T>(new Pager<K> { PageIndex = pageIndex, PageSize = pageSize }, where, orderBy, isAsc);
        //            return new ActionResultData<Pager<T>>(1, pager);
        //        }

        #endregion
    }
}