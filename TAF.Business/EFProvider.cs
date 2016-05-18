namespace TAF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using EntityFramework.Caching;
    using EntityFramework.Extensions;

    using TAF.Utility;

    /// <summary>
    /// EF数据提供者
    /// </summary>
    public class EFProvider : IDbProvider
       
    {
        #region 构造函数

        /// <summary>
        /// Initializes a new instance of the <see cref="EfBusiness{K}"/> class.
        /// </summary>
        public EFProvider()
        {
            this.DbContext = Ioc.Create<DbContext>();
        }

        #endregion

        #region 查询

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
        /// <typeparam name="K"></typeparam>
        /// <returns>
        /// The <see cref="pager"/>.
        /// </returns>
        public Pager<T> Pages<K,R, T>(
            Pager<T> pager,
            Func<K, bool> whereFunc,
            Func<K, R> orderByFunc,
            bool isAsc = true,
            bool useCache = false) where K : BaseBusiness<K>, new() where T : new()
        {
            var context = Ioc.Create<DbContext>();
            var set = context.Set<K>();
            var query = useCache ? set.FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromDays(1))).AsQueryable() : set;
            pager.Load(query.AsEnumerable(), whereFunc, orderByFunc, isAsc);
            return pager;
        }

        /// <summary>
        /// 查询对象列表
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="query">
        /// The func.
        /// </param>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// </returns>
        public List<K> Get<K>(Expression<Func<K, bool>> query, bool useCache = false) where K : BaseBusiness<K>, new()
        {
            var dbContext = Ioc.Create<DbContext>();
            var items = useCache
                ? dbContext.Set<K>().Where(query).FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromDays(1))).ToList()
                : dbContext.Set<K>().Where(query).ToList();
            return items;
        }

        /// <summary>
        /// 条件查询单一对象
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <param name="useCache">
        /// 是否使用缓存
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        public K Find<K>(Expression<Func<K, bool>> func, bool useCache = false) where K : BaseBusiness<K>, new()
        {
            var dbContext = Ioc.Create<DbContext>();
            var query = dbContext.Set<K>();
            var item = useCache
                ? query.FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromDays(1))).AsQueryable().FirstOrDefault(func)
                : query.FirstOrDefault(func);
            return item;
        }

        #endregion

        /// <summary>
        /// 是否存在满足条件的对象
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Exist<K>(Expression<Func<K, bool>> func) where K : BaseBusiness<K>, new()
        {
            return Ioc.Create<DbContext>().Set<K>().Any(func);
        }

        /// <summary>
        /// 查询列表数量
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="func">
        /// 过滤条件
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Count<K>(Expression<Func<K, bool>> func) where K : BaseBusiness<K>, new()
        {
            return Ioc.Create<DbContext>().Set<K>().Count(func);
        }

        /// <summary>
        /// 插入对象到数据库
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="item">
        /// </param>
        /// <param name="commit">
        /// 是否提交
        /// </param>
        /// <returns>
        /// </returns>
        public int Add<K>(K item, bool commit) where K : BaseBusiness<K>, new()
        {
            this.DbContext.Set<K>().Add(item);
            if (commit)
            {
                return this.DbContext.SaveChanges();
            }
            return 0;
        }

        /// <summary>
        /// 根据对象Id删除对象
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Delete<K>(Guid id) where K : BaseBusiness<K>, new()
        {
            return Ioc.Create<DbContext>().Set<K>().Where(r => r.Id == id).Delete();
        }

        /// <summary>
        /// 根据条件删除对象
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="func">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Delete<K>(Expression<Func<K, bool>> func) where K : BaseBusiness<K>, new()
        {
            return Ioc.Create<DbContext>().Set<K>().Where(func).Delete();
        }

        /// <summary>
        /// 根据条件更新对象
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="func">
        /// </param>
        /// <param name="update">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Update<K>(Expression<Func<K, bool>> func, Expression<Func<K, K>> update) where K : BaseBusiness<K>, new()
        {
            return Ioc.Create<DbContext>().Set<K>().Where(func).Update(update);
        }

        /// <summary>
        /// 提交到数据库
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return this.DbContext.SaveChanges();
        }

        public void LoadDbContext(DbContext context)
        {
            this.DbContext = context;
        }

        /// <summary>
        /// EF数据库对象
        /// </summary>
        public DbContext DbContext
        {
            get; set;
        }
    }
}