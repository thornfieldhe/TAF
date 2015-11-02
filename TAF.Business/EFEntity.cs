// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EFEntity.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the EFEntity type.
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
    using Core;
    using EntityFramework.Caching;
    using EntityFramework.Extensions;

    using TAF.Entity;
    using TAF.Utility;

    /// <summary>
    /// The ef entity.
    /// </summary>
    /// <typeparam name="K">
    /// </typeparam>
    public abstract class EfBusiness<K> : BaseBusiness<K> where K : EfBusiness<K>, IBusinessBase, new()
    {
        #region 构造函数

        /// <summary>
        /// Initializes a new instance of the <see cref="EfBusiness{K}"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        protected EfBusiness(Guid id)
            : base(id)
        {
            DbContex = Ioc.Create<IContextWapper>().Context;
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EfBusiness{K}"/> class.
        /// </summary>
        protected EfBusiness() : this(Guid.NewGuid())
        {
        }

        #endregion

        /// <summary>
        /// Gets or sets the db contex.
        /// </summary>
        [NotMapped]
        protected DbContext DbContex
        {
            get; set;
        }

        #region 静态方法

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        public static K Get(Guid id)
        {
            return new K().QuerySingle(i => i.Id == id);
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<K> GetAll(bool useCache = false)
        {
            return new K().Query(useCache);
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// </returns>
        public static List<K> Get(Expression<Func<K, bool>> func, bool useCache = false)
        {
            return new K().Query(func, useCache);
        }

        /// <summary>
        /// The pages.
        /// </summary>
        /// <param name="pager">
        /// The pager.
        /// </param>
        /// <param name="whereFunc">
        /// The where func.
        /// </param>
        /// <param name="orderByFunc">
        /// The order by func.
        /// </param>
        /// <param name="isAsc">
        /// The is asc.
        /// </param>
        /// <typeparam name="R">
        /// </typeparam>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="pager"/>.
        /// </returns>
        public static Pager<T> Pages<R, T>(
            Pager<T> pager,
            Func<K, bool> whereFunc,
            Func<K, R> orderByFunc,
            bool isAsc = true) where T : new()
        {
            var context = Ioc.Create<IContextWapper>().Context;
            pager.Load(context.Set<K>().AsEnumerable(), whereFunc, orderByFunc, isAsc);
            return pager;
        }

        public static Pager<T> Pages<T>(Pager<T> pager, bool isAsc = true) where T : new()
        {
            var context = Ioc.Create<IContextWapper>().Context;
            pager.Load(context.Set<K>().AsEnumerable(), isAsc);
            return pager;
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        public static K GetById(Guid id)
        {
            var context = Ioc.Create<IContextWapper>().Context;
            var item = context.Set<K>().Find(id);
            item.DbContex = context;
            return item;
        }

        /// <summary>
        /// The exist.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Exist(Expression<Func<K, bool>> func)
        {
            return Ioc.Create<IContextWapper>().Context.Set<K>().Any(func);
        }

        /// <summary>
        /// The count.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int Count(Expression<Func<K, bool>> func)
        {
            return Ioc.Create<IContextWapper>().Context.Set<K>().Count(func);
        }

        #endregion

        #region 实例方法

        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int Create()
        {
            PreInsert();
            Validate();
            Insert();
            PostInsert();
            return DbContex.SaveChanges();
        }

        /// <summary>
        /// The save.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int Save()
        {
            PreUpdate();
            Validate();
            Update();
            PostUpdate();
            return DbContex.SaveChanges();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int Delete()
        {
            PreRemove();
            Remove();
            PostRemove();
            return DbContex.SaveChanges();
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        public K Find(Guid id)
        {
            return QuerySingle(i => i.Id == id);
        }

        #region 模块内部方法

        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        internal List<K> Query(Expression<Func<K, bool>> func, bool useCache = false)
        {
            var query = DbContex.Set<K>().Where(func);
            var items = PreQuery(query, useCache);
            return PostQuery(items);
        }

        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        internal List<K> Query(bool useCache = false)
        {
            var query = DbContex.Set<K>();
            var items = PreQuery(query, useCache);
            return PostQuery(items);
        }

        /// <summary>
        /// The query single.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        internal K QuerySingle(Expression<Func<K, bool>> func)
        {
            var query = DbContex.Set<K>().Where(func);
            var item = PreQuerySingle(query);
            return PostQuerySingle(item);
        }

        #endregion


        #region 继承方法
        /// <summary>
        /// The insert.
        /// </summary>
        protected override void Insert()
        {
            DbContex.Set<K>().Add(this as K);
        }

        /// <summary>
        /// The remove.
        /// </summary>
        protected override void Remove()
        {
            //软删除
            //            this.Status = -1;
            //            DbContex.SaveChanges();
            //硬删除
            DbContex.Set<K>().Remove(this as K);
        }

        /// <summary>
        /// The pre query.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        protected override List<K> PreQuery(IQueryable<K> query, bool useCache = false)
        {
            var items = useCache
                            ? query.FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(60))).ToList()
                            : query.ToList();
            return PostQuery(items);
        }

        /// <summary>
        /// The post query.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        protected override List<K> PostQuery(List<K> items)
        {
            items.ForEach(i => i.DbContex = DbContex);
            return items;
        }

        /// <summary>
        /// The post query single.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        protected override K PostQuerySingle(K item)
        {
            item.IfNotNull(i => i.DbContex = DbContex);
            return item;
        }

        #endregion

        #endregion
    }
}
