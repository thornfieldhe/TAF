// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEntity.Base.cs" company="">
//   
// </copyright>
// <summary>
//   业务类基类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq.Expressions;

    using AutoMapper;

    using TAF.Core;

    /// <summary>
    /// The base business.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract partial class BaseBusiness<T> : IDbAction
    {
        protected BaseBusiness(IDbProvider dbProvider) : this()
        {
            this.DbProvider = dbProvider;
        }

        private static IDbProvider provider;

        public static IDbProvider Provider
        {
            get
            {
                return provider ?? (provider = Ioc.Create<IDbProvider>());
            }

            set
            {
                provider = value ?? Ioc.Create<IDbProvider>();
            }
        }

        [NotMapped]
        public IDbProvider DbProvider
        {
            get; set;
        }

        #region 静态方法

        #region 查询

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="useCache">
        /// 是否使用缓存
        /// </param>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        public static List<T> GetAll(bool useCache = false)
        {
            var result = Provider.Get<T>(r => true, useCache);
            result.ForEach(
                r =>
                {
                    r.MarkClean();
                    r.DbProvider = Provider;
                });
            return result;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <typeparam name="K">
        /// 对象视图
        /// </typeparam>
        /// <param name="useCache">
        /// 是否使用缓存
        /// </param>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        public static List<K> GetAll<K>(bool useCache = false) where K : IEntityBase
        {
            var result = GetAll(useCache);
            return result != null ? Mapper.Map<List<T>, List<K>>(result) : new List<K>();
        }

        /// <summary>
        /// 条件查询数据
        /// </summary>
        /// <param name="func">
        /// 过滤条件
        /// </param>
        /// <param name="useCache">
        /// 是否使用缓存
        /// </param>
        /// <returns>
        /// </returns>
        public static List<T> Get(Expression<Func<T, bool>> func, bool useCache = false)
        {
            var result = Provider.Get(func, useCache);
            result.ForEach(
                r =>
                    {
                        r.MarkClean();
                        r.DbProvider = Provider;
                    });
            return result;
        }

        /// <summary>
        /// 条件查询数据
        /// </summary>
        /// <typeparam name="K"/>
        /// 对象列表视图
        /// <param name="func">
        /// 过滤条件
        /// </param>
        /// <param name="useCache">
        /// 是否使用缓存
        /// </param>
        /// <returns>
        /// </returns>
        public static List<K> Get<K>(Expression<Func<T, bool>> func, bool useCache = false) where K : IEntityBase
        {
            var result = Get(func, useCache);
            return result != null ? Mapper.Map<List<T>, List<K>>(result) : new List<K>();
        }

        /// <summary>
        /// 分页查询数据
        /// 转换成对象T的列表
        /// </summary>
        /// <param name="pager">
        /// 分页对象
        /// </param>
        /// <param name="whereFunc">
        /// 过滤条件
        /// </param>
        /// <param name="orderByFunc">
        /// 排序属性
        /// </param>
        /// <param name="isAsc">
        /// 是否是顺序
        /// </param>
        /// <param name="useCache">
        /// 是否使用缓存
        /// </param>
        /// <typeparam name="R">
        /// 排序对象
        /// </typeparam>
        /// <typeparam name="T">
        /// 结果转换为List<T>输出
        /// </typeparam>
        /// <returns>
        /// The <see cref="pager"/>.
        /// </returns>
        public static Pager<T> Pages<R, T>(
            Pager<T> pager,
            Func<T, bool> whereFunc,
            Func<T, R> orderByFunc,
            bool isAsc = true,
            bool useCache = false) where T : BaseBusiness<T>, new()
        {
            return Provider.Pages<T, R>(pager, whereFunc, orderByFunc, isAsc, useCache);
        }

        /// <summary>
        /// 分页查询数据
        /// 转换成对象T的列表
        /// </summary>
        /// <param name="pager">
        /// 分页对象
        /// </param>
        /// <param name="whereFunc">
        /// 过滤条件
        /// </param>
        /// <param name="orderByFunc">
        /// 排序属性
        /// </param>
        /// <param name="isAsc">
        /// 是否是顺序
        /// </param>
        /// <param name="useCache">
        /// 是否使用缓存
        /// </param>
        /// <typeparam name="R">
        /// 排序对象
        /// </typeparam>
        /// <typeparam name="T">
        /// 结果转换为List<T>输出
        /// </typeparam>
        /// <typeparam name="K">
        /// 对象列表视图
        /// </typeparam>
        /// <returns>
        /// The <see cref="pager"/>.
        /// </returns>
        public static Pager<K> Pages<R, T, K>(
            Pager<T> pager,
            Func<T, bool> whereFunc,
            Func<T, R> orderByFunc,
            bool isAsc = true,
            bool useCache = false) where T : BaseBusiness<T>, new() where K : IEntityBase, new()
        {
            var result = Pages(pager, whereFunc, orderByFunc, isAsc, useCache);
            var newPager = new Pager<K>()
            {
                Datas = new List<K>(),
                PageIndex = 1,
                PageSize = 1,
                ShowIndex = new List<int>() { 1 },
                Total = 1
            };

            if (result == null)
                return newPager;

            newPager.Datas = Mapper.Map<List<T>, List<K>>(result.Datas);
            newPager.PageIndex = result.PageIndex;
            newPager.PageSize = result.PageSize;
            newPager.ShowIndex = result.ShowIndex;
            newPager.Total = result.Total;
            return newPager;
        }

        /// <summary>
        /// 根据主键查询单条数据
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="useCache">
        /// 是否使用缓存
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T Find(Guid id, bool useCache = false)
        {
            var item = Provider.Find<T>(r => r.Id == id, useCache);
            if (item == null)
            {
                return null;
            }

            item.DbProvider = Provider;
            item.MarkClean();
            return item;
        }

        /// <summary>
        /// 根据主键查询单条数据
        /// </summary>
        /// <typeparam name="K">
        /// 对象视图
        /// </typeparam>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="useCache">
        /// 是否使用缓存
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static K Find<K>(Guid id, bool useCache = false) where K : class, IEntityBase
        {
            var result = Find(id, useCache);
            return result != null ? Mapper.Map<T, K>(result) : null;
        }

        /// <summary>
        /// 条件查询单条数据
        /// </summary>
        /// <param name="func">
        /// The id.
        /// </param>
        /// <param name="useCache">
        /// 是否使用缓存
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T Find(Expression<Func<T, bool>> func, bool useCache = false)
        {
            var item = Provider.Find(func, useCache);
            if (item == null)
            {
                return null;
            }

            item.DbProvider = Provider;
            item.MarkClean();
            return item;
        }

        /// <summary>
        /// 条件查询单条数据
        /// </summary>
        /// <typeparam name="K">
        /// 对象视图
        /// </typeparam>
        /// <param name="func">
        /// The id.
        /// </param>
        /// <param name="useCache">
        /// 是否使用缓存
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static K Find<K>(Expression<Func<T, bool>> func, bool useCache = false) where K : class, IEntityBase
        {
            var result = Find(func, useCache);
            return result != null ? Mapper.Map<T, K>(result) : null;
        }
        #endregion

        /// <summary>
        /// 是否存在满足条件的对象
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Exist(Expression<Func<T, bool>> func)
        {
            return Provider.Exist(func);
        }

        /// <summary>
        /// 查询列表数量
        /// </summary>
        /// <param name="func">
        /// 过滤条件
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int Count(Expression<Func<T, bool>> func)
        {
            return Provider.Count(func);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <param name="commit"></param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int Add(T t, bool commit = true)
        {
            return Provider.Add(t, commit);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="ts">
        /// The ts.
        /// </param>
        /// <param name="commit">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int AddRange(IEnumerable<T> ts, bool commit)
        {
            return Provider.AddRange(ts, commit);
        }

        /// <summary>
        /// 根据对象Id删除对象
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <param name="commit"></param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int DeleteById(Guid id, bool commit)
        {
            return Provider.Delete<T>(id, commit);
        }

        /// <summary>
        /// 根据条件删除对象
        /// </summary>
        /// <param name="func">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int Delete(Expression<Func<T, bool>> func)
        {
            return Provider.Delete(func);
        }

        /// <summary>
        /// 根据条件更新对象
        /// </summary>
        /// <param name="func">
        /// </param>
        /// <param name="update">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int Update(Expression<Func<T, bool>> func, Expression<Func<T, T>> update)
        {
            return Provider.Update(func, update);
        }

        #endregion

        #region 实例方法

        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        /// <param name="commit">
        /// The commit.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Create(Guid userId, bool commit = true)
        {
            var result = 0;
            PreInsert(userId);
            result += this.Insert(commit);
            this.PostInsert();
            return result;
        }

        /// <summary>
        /// 更新一个对象
        /// </summary>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        /// <param name="commit">
        /// The commit.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Save(Guid userId, bool commit = true)
        {
            var result = 0;
            PreUpdate(userId);
            result += Update(commit);
            PostUpdate();
            return result;
        }

        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        /// <param name="commit">
        /// The commit.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int SoftDelete(Guid userId, bool commit = true)
        {
            var result = 0;
            PreRemove(userId);
            result += SoftRemove(commit);
            PostRemove();
            return result;
        }

        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        /// <param name="commit">
        /// The commit.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Delete(Guid userId, bool commit = true)
        {
            var result = 0;
            PreRemove(userId);
            result += Remove(commit);
            PostRemove();
            return result;
        }

        /// <summary>
        /// 直接提交一个对象，不用考虑是新增还是更新还是删除
        /// </summary>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        /// <param name="commit">
        /// The commit.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Submit(Guid userId, bool commit = true)
        {
            if (this.IsNew)
            {
                return Create(userId, commit);
            }
            else if (this.IsDirty)
            {
                return Save(userId, commit);
            }
            else if (IsDelete)
            {
                return SoftDelete(userId, commit);
            }

            return 0;
        }


        #region 继承方法

        #region 插入

        /// <summary>
        /// 在插入之前给对象赋值
        /// </summary>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        protected virtual void PreInsert(Guid userId)
        {
            if (this.Id == Guid.Empty)
            {
                this.Id = Guid.NewGuid();
            }

            this.CreatedBy = userId;
            this.ModifyBy = userId;
            this.CreatedDate = DateTime.Now;
            this.ChangedDate = DateTime.Now;
            this.MarkNew();
        }

        /// <summary>
        /// 插入对象到数据库
        /// </summary>
        /// <param name="commit">
        /// The commit.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int Insert(bool commit = true)
        {
            return this.DbProvider.Add(this as T, commit);
        }


        /// <summary>
        /// 在插入之后操作
        /// </summary>
        protected virtual void PostInsert()
        {
        }

        #endregion

        #region 更新

        /// <summary>
        /// 更新之前给对象赋值
        /// </summary>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        protected virtual void PreUpdate(Guid userId)
        {
            ChangedDate = DateTime.Now;
            ModifyBy = userId;
        }

        /// <summary>
        /// 更新对象到数据库
        /// </summary>
        /// <param name="commit">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int Update(bool commit = true)
        {
            return (this.IsDirty && commit) ? this.DbProvider.Commit() : 0;
        }

        /// <summary>
        /// 更新对象到数据库后执行操作
        /// </summary>
        protected virtual void PostUpdate()
        {
        }

        #endregion

        #region 移除

        /// <summary>
        /// 数据库移除对象前执行操作
        /// </summary>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        protected virtual void PreRemove(Guid userId)
        {
            this.ModifyBy = userId;
        }

        /// <summary>
        /// 从数据库移除对象
        /// </summary>
        /// <param name="commit"></param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int Remove(bool commit = true)
        {
            this.DbProvider.Delete(this as T, commit);
            if (commit)
            {
                return this.DbProvider.Commit();
            }

            MarkDelete();
            return 0;
        }

        /// <summary>
        /// 从数据库移除对象
        /// </summary>
        /// <param name="commit"></param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int SoftRemove(bool commit = true)
        {
            this.Status = -1;
            if (commit)
            {
                return this.DbProvider.Commit();
            }

            MarkDelete();
            return 0;
        }

        /// <summary>
        /// 数据库移除对象后执行操作
        /// </summary>
        protected virtual void PostRemove()
        {
            this.MarkDelete();
        }

        #endregion


        #endregion

        #endregion

    }
}