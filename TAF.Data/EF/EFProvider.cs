namespace TAF.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using EntityFramework.Caching;
    using EntityFramework.Extensions;
    using EntityFramework.Future;
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
            //            this.DbContext.EnableFilter("Status");
            //            this.DbContext.SetFilterScopedParameterValue("Status", -1);
            //            this.DbContext.EnableFilter("CreatedBy").SetParameter("userId", "1");
            //            this.DbContext.EnableFilter("ModifyBy").SetParameter("userId", "1");
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
        public Pager<T> Pages<K, R, T>(
            Pager<T> pager,
            Func<K, bool> whereFunc,
            Func<K, R> orderByFunc,
            bool isAsc = true,
            bool useCache = false) where K : BaseBusiness<K>, new() where T : new() where R : new()
        {
            var set = this.DbContext.Set<K>();
            var query = useCache ? set.FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromDays(1))).AsQueryable() : set;
            return Load<R, K, T>(pager, query, whereFunc, orderByFunc, isAsc);
        }

        /// <summary>
        /// 分页查询对象列表
        /// </summary>
        /// <typeparam name="K">
        /// 查询对象
        /// </typeparam>
        /// <typeparam name="R">
        /// </typeparam>
        /// <param name="pager">
        /// The pager.
        /// </param>
        /// <param name="whereFunc">
        /// The where Func.
        /// </param>
        /// <param name="orderByFunc">
        /// The order By Func.
        /// </param>
        /// <param name="isAsc">
        /// 是否是顺序
        /// </param>
        /// <param name="useCache">
        /// The use Cache.
        /// </param>
        /// <returns>
        /// The <see cref="Pager"/>.
        /// </returns>
        public Pager<K> Pages<K, R>(Pager<K> pager, Func<K, bool> whereFunc, Func<K, R> orderByFunc, bool isAsc = true, bool useCache = false) where K : BaseBusiness<K>, new()
        {
            var set = this.DbContext.Set<K>();
            var query = useCache
                            ? set.FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromDays(1)))
                                  .AsQueryable()
                            : set;
            return Load<K, R>(pager, query.AsEnumerable(), whereFunc, orderByFunc, isAsc);
        }

        /// <summary>
        /// 分页查询对象列表
        /// </summary>
        /// <typeparam name="R">
        /// 排序字段类型
        /// </typeparam>
        /// <typeparam name="K">
        /// 查询对象
        /// </typeparam>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="pager">
        /// </param>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="whereFunc">
        /// 条件表达式
        /// </param>
        /// <param name="orderByFunc">
        /// 排序表达式
        /// </param>
        /// <param name="isAsc">
        /// 是否是顺序
        /// </param>
        /// <returns>
        /// The <see cref="Pager"/>.
        /// </returns>
        private Pager<T> Load<R, K, T>(Pager<T> pager, IEnumerable<K> query, Func<K, bool> whereFunc, Func<K, R> orderByFunc, bool isAsc) where K : BaseBusiness<K>, new() where R : new() where T : new()
        {

            var pagerK = new Pager<K>(pager.PageIndex, pager.PageSize);
            pagerK = Load<K, R>(pagerK, query, whereFunc, orderByFunc, isAsc);
            pager = Mapper.Map<Pager<T>>(pagerK);
            pager.Datas = Mapper.Map<List<T>>(pagerK.Datas);

            //            pagerT.Total = pagerK.Total;
            //            pagerT.PageIndex = pagerK.PageIndex;
            //            pagerT.PageSize = pagerK.PageSize;
            //            pagerT.ShowIndex = pagerK.ShowIndex;

            return pager;
        }

        /// <summary>
        /// 分页查询对象列表
        /// </summary>
        /// <typeparam name="K">
        /// 查询对象
        /// </typeparam>
        /// <typeparam name="R">
        /// </typeparam>
        /// <param name="pager">
        /// </param>
        /// <param name="query">
        /// 查询表达式
        /// </param>
        /// <param name="whereFunc">
        /// The where Func.
        /// </param>
        /// <param name="orderByFunc">
        /// The order By Func.
        /// </param>
        /// <param name="isAsc">
        /// 是否是顺序
        /// </param>
        /// <returns>
        /// The <see cref="Pager"/>.
        /// </returns>
        public Pager<K> Load<K, R>(Pager<K> pager, IEnumerable<K> query, Func<K, bool> whereFunc, Func<K, R> orderByFunc, bool isAsc) where K : BaseBusiness<K>, new()
        {
            pager.Total = query.AsQueryable().FutureCount();
            FutureQuery<K> result;
            if (isAsc)
            {
                result =
                    query.Where(whereFunc).OrderBy(orderByFunc)
                        .Skip(pager.PageSize * (pager.PageIndex - 1))
                        .Take(pager.PageSize)
                        .AsQueryable()
                        .Future();
            }
            else
            {
                result =
                    query.Where(whereFunc).OrderBy(orderByFunc)
                        .Skip(pager.PageSize * (pager.PageIndex - 1))
                        .Take(pager.PageSize)
                        .AsQueryable()
                        .Future();
            }

            pager.Datas = result.ToList();
            pager.GetShowIndex();
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
            var items = useCache
                ? this.DbContext.Set<K>().Where(query).FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromDays(1))).ToList()
                : this.DbContext.Set<K>().Where(query).ToList();
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
            var query = this.DbContext.Set<K>();
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
            return this.DbContext.Set<K>().Any(func);
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
            return this.DbContext.Set<K>().Count(func);
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
        /// <param name="allowCommit"></param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Delete<K>(Guid id, bool allowCommit) where K : BaseBusiness<K>, new()
        {
            return this.DbContext.Set<K>().Where(r => r.Id == id).Delete();
        }

        /// <summary>
        /// 根据对象Id删除对象
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="item"></param>
        /// <param name="allowCommit"></param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Delete<K>(K item, bool allowCommit = true) where K : BaseBusiness<K>, new()
        {
            this.DbContext.Set<K>().Remove(item);
            if (allowCommit)
            {
                return this.DbContext.SaveChanges();
            }
            return 0;
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
            return this.DbContext.Set<K>().Where(func).Delete();
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
            return this.DbContext.Set<K>().Where(func).Update(update);
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