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
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using EntityFramework.Caching;
    using EntityFramework.Extensions;

    using TAF.Utility;

    /// <summary>
    /// The base business.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract partial class BaseBusiness<T>
    {
        protected BaseBusiness(IDbProvider<T> dbProvider)
        {
            this.DbProvider = dbProvider;
        }

        public IDbProvider<T> DbProvider
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
            return Ioc.Create<IDbProvider<T>>().Query(r => true, useCache);
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
            return Ioc.Create<IDbProvider<T>>().Query(func, useCache);
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
            bool useCache = false) where T : new()
        {
            return Ioc.Create<IDbProvider<T>>().Pages(pager, whereFunc, orderByFunc, isAsc, useCache);
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
            var provider = Ioc.Create<IDbProvider<T>>();
            var item = provider.Find(id, useCache);
            item.DbProvider = provider;
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
            var provider = Ioc.Create<IDbProvider<T>>();
            var item = provider.QuerySingle(func, useCache);
            item.DbProvider = provider;
            return item;
        }

        /// <summary>
        /// 查询对象列表
        /// </summary>
        /// <param name="query">
        /// The func.
        /// </param>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// </returns>
        private static List<T> Query(Expression<Func<T, bool>> query, bool useCache = false)
        {
            var provider = Ioc.Create<IDbProvider<T>>();
            var items = provider.Query(query, useCache);
            items.ForEach(
                i =>
                {
                    i.DbProvider = provider;
                    i.MarkOld();
                });
            return items;
        }

        /// <summary>
        /// 条件查询单一对象
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <param name="useCache">
        /// 是否使用缓存
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        private static T QuerySingle(Expression<Func<T, bool>> func, bool useCache = false)
        {
            var dbContext = Ioc.Create<DbContext>();
            var query = dbContext.Set<T>();
            var item = useCache
                ? query.FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromDays(1))).AsQueryable().FirstOrDefault(func)
                : query.FirstOrDefault(func);
            item.IfNotNull(
          i =>
          {
              i.DbContext = dbContext;
              i.MarkOld();
          });
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
            return Ioc.Create<DbContext>().Set<T>().Any(func);
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
            return Ioc.Create<DbContext>().Set<T>().Count(func);
        }

        /// <summary>
        /// 根据对象Id删除对象
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(Guid id)
        {
            Ioc.Create<DbContext>().Set<T>().Where(r => r.Id == id).Delete();
        }

        /// <summary>
        /// 根据条件删除对象
        /// </summary>
        /// <param name="func"></param>
        public static void Delete(Expression<Func<T, bool>> func)
        {
            Ioc.Create<DbContext>().Set<T>().Where(func).Delete();
        }

        /// <summary>
        /// 根据条件更新对象
        /// </summary>
        /// <param name="func"></param>
        /// <param name="update"></param>
        public static void Update(Expression<Func<T, bool>> func, Expression<Func<T, T>> update)
        {
            Ioc.Create<DbContext>().Set<T>().Where(func).Update(update);
        }

        #endregion

        #region 实例方法

        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Create()
        {
            var result = 0;
            PreInsert();
            Validate();
            result += Insert();
            result += PostInsert();
            return result;
        }

        /// <summary>
        /// 更新一个对象
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Save()
        {
            var result = 0;
            PreUpdate();
            Validate();
            result += Update();
            result += PostUpdate();
            return result;
        }


        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Delete()
        {
            var result = 0;
            PreRemove();
            result += this.Remove();
            result += PostRemove();
            return result;
        }


        #region 继承方法

        #region 插入

        /// <summary>
        /// 在插入之前给对象赋值
        /// </summary>
        protected virtual void PreInsert()
        {
            this.Id = Guid.NewGuid();
            this.CreatedDate = DateTime.Now;
            this.ChangedDate = DateTime.Now;
            this.MarkNew();
        }

        /// <summary>
        /// 插入对象到数据库
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int Insert()
        {
            this.DbContext.Set<T>().Add(this as T);
            return this.DbContext.SaveChanges();
        }


        /// <summary>
        /// 在插入之后操作
        /// </summary>
        /// <returns>
        /// </returns>
        protected virtual int PostInsert()
        {
            return 0;
        }

        #endregion

        #region 更新

        /// <summary>
        /// 更新之前给对象赋值
        /// </summary>
        protected virtual void PreUpdate()
        {
            ChangedDate = DateTime.Now;
            this.MarkDirty();
        }

        /// <summary>
        /// 更新对象到数据库
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int Update()
        {
            return !IsClean ? this.DbContext.SaveChanges() : 0;
        }

        /// <summary>
        /// 更新对象到数据库后执行操作
        /// </summary>
        /// <returns>
        /// </returns>
        protected virtual int PostUpdate()
        {
            return 0;
        }

        #endregion

        #region 移除

        /// <summary>
        /// 数据库移除对象前执行操作
        /// </summary>
        /// <returns>
        /// </returns>
        protected virtual int PreRemove()
        {
            this.MarkDelete();
            return 0;
        }

        /// <summary>
        /// 从数据库移除对象
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int Remove()
        {
            //软删除
            //            this.Status = -1;
            //            DbContext.SaveChanges();
            //硬删除
            this.DbContext.Set<T>().Remove(this as T);
            return this.DbContext.SaveChanges();
        }


        /// <summary>
        /// 数据库移除对象后执行操作
        /// </summary>
        /// <returns>
        /// </returns>
        protected virtual int PostRemove()
        {
            return 0;
        }

        #endregion


        #endregion

        #endregion

        public void LoadDbContext(DbContext context)
        {
            this.DbContext = context;
        }

        /// <summary>
        /// EF数据库对象
        /// </summary>
        [NotMapped]
        protected DbContext DbContext
        {
            get; set;
        }
    }
}