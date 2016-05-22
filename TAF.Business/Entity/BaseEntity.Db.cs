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
    using System.Linq.Expressions;

    using TAF.Core;

    /// <summary>
    /// The base business.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract partial class BaseBusiness<T> : IDbAction
    {
        protected BaseBusiness(IDbProvider dbProvider)
        {
            this.DbProvider = dbProvider;
        }

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
            var provider = Ioc.Create<IDbProvider>();
            var result = provider.Get<T>(r => true, useCache);
            result.ForEach(
                r =>
                {
                    r.MarkClean();
                    r.DbProvider = provider;
                });
            return result;
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
            var provider = Ioc.Create<IDbProvider>();
            var result = provider.Get(func, useCache);
            result.ForEach(
                r =>
                    {
                        r.MarkClean();
                        r.DbProvider = provider;
                    });
            return result;
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
            return Ioc.Create<IDbProvider>().Pages<T, R>(pager, whereFunc, orderByFunc, isAsc, useCache);
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
            var provider = Ioc.Create<IDbProvider>();
            var item = provider.Find<T>(r => r.Id == id, useCache);
            if (item == null)
            {
                return null;
            }

            item.DbProvider = provider;
            item.MarkClean();
            return item;
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
            var provider = Ioc.Create<IDbProvider>();
            var item = provider.Find(func, useCache);
            if (item == null)
            {
                return null;
            }

            item.DbProvider = provider;
            item.MarkClean();
            return item;
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
            var provider = Ioc.Create<IDbProvider>();
            return provider.Exist(func);
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
            var provider = Ioc.Create<IDbProvider>();
            return provider.Count(func);
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
        public static int Delete(Guid id, bool commit)
        {
            var provider = Ioc.Create<IDbProvider>();
            return provider.Delete<T>(id, commit);
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
            var provider = Ioc.Create<IDbProvider>();
            return provider.Delete(func);
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
            var provider = Ioc.Create<IDbProvider>();
            return provider.Update(func, update);
        }

        #endregion

        #region 实例方法

        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="commit">
        /// The commit.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Create(bool commit = true)
        {
            var result = 0;
            PreInsert();
            Validate();
            result += Insert(commit);
            PostInsert();
            return result;
        }

        /// <summary>
        /// 更新一个对象
        /// </summary>
        /// <param name="commit">
        /// The commit.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Save(bool commit = true)
        {
            var result = 0;
            PreUpdate();
            Validate();
            result += Update(commit);
            PostUpdate();
            return result;
        }

        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="commit">
        /// The commit.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int SoftDelete(bool commit = true)
        {
            var result = 0;
            PreRemove();
            result += SoftRemove(commit);
            PostRemove();
            return result;
        }

        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="commit">
        /// The commit.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Delete(bool commit = true)
        {
            var result = 0;
            PreRemove();
            result += Remove(commit);
            PostRemove();
            return result;
        }

        /// <summary>
        /// 直接提交一个对象，不用考虑是新增还是更新还是删除
        /// </summary>
        /// <param name="commit">
        /// The commit.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Submit(bool commit = true)
        {
            if (this.IsNew)
            {
                Create(commit);
            }
            else if (this.IsDirty)
            {
                Update(commit);
            }
            else if (IsDelete)
            {
                SoftDelete(commit);
            }

            return 0;
        }


        #region 继承方法

        #region 插入

        /// <summary>
        /// 在插入之前给对象赋值
        /// </summary>
        protected virtual void PreInsert()
        {
            if (this.Id == Guid.Empty)
            {
                this.Id = Guid.NewGuid();
            }
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
        protected virtual void PreUpdate()
        {
            ChangedDate = DateTime.Now;
        }

        /// <summary>
        /// 更新对象到数据库
        /// </summary>
        /// <param name="commit"></param>
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
        protected virtual void PreRemove()
        {
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