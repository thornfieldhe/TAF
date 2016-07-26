namespace TAF
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// 数据库提供者接口
    /// </summary>
    public interface IDbProvider
    {
        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="query">
        /// 过滤条件
        /// </param>
        /// <param name="useCache">
        /// 是否从缓存中查询
        /// </param>
        /// <returns>
        /// </returns>
        List<K> Get<K>(Expression<Func<K, bool>> query, bool useCache = false) where K : BaseBusiness<K>, new();

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="func">
        /// 过滤条件
        /// </param>
        /// <param name="useCache">
        /// 是否从缓存中查询
        /// </param>
        /// <returns>
        /// </returns>
        K Find<K>(Expression<Func<K, bool>> func, bool useCache = false) where K : BaseBusiness<K>, new();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <typeparam name="R">
        /// 排序对象
        /// </typeparam>
        /// <typeparam name="T">
        /// 结果转换为T的列表输出
        /// </typeparam>
        /// <param name="pager"></param>
        /// <param name="whereFunc">
        /// 过滤条件
        /// </param>
        /// <param name="orderByFunc">
        /// 排序条件
        /// </param>
        /// <param name="isAsc">
        /// 是否是顺序
        /// </param>
        /// <param name="useCache">
        /// 是否从缓存中读取
        /// </param>
        /// <returns>
        /// </returns>
        Pager<T> Pages<K, R, T>(
            Pager<T> pager,
            Func<K, bool> whereFunc,
            Func<K, R> orderByFunc,
            bool isAsc = true,
            bool useCache = false) where K : BaseBusiness<K>, new() where T : new() where R : new();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <typeparam name="R">
        /// 排序对象
        /// </typeparam>
        /// <param name="pager">
        /// </param>
        /// <param name="whereFunc">
        /// 过滤条件
        /// </param>
        /// <param name="orderByFunc">
        /// 排序条件
        /// </param>
        /// <param name="isAsc">
        /// 是否是顺序
        /// </param>
        /// <param name="useCache">
        /// </param>
        /// <returns>
        /// </returns>
        Pager<K> Pages<K, R>(Pager<K> pager, Func<K, bool> whereFunc, Func<K, R> orderByFunc, bool isAsc = true, bool useCache = false) where K : BaseBusiness<K>, new();

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="func">
        /// 过滤条件
        /// </param>
        /// <returns>
        /// </returns>
        bool Exist<K>(Expression<Func<K, bool>> func) where K : BaseBusiness<K>, new();

        /// <summary>
        /// 查询数据条数
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="func">
        /// 过滤条件
        /// </param>
        /// <returns>
        /// </returns>
        int Count<K>(Expression<Func<K, bool>> func) where K : BaseBusiness<K>, new();

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="id">
        /// </param>
        /// <param name="allowCommit"></param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Delete<K>(Guid id, bool allowCommit = true) where K : BaseBusiness<K>, new();

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="func">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Delete<K>(Expression<Func<K, bool>> func) where K : BaseBusiness<K>, new();

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="item"></param>
        /// <param name="allowCommit"></param>
        /// <returns></returns>
        int Delete<K>(K item, bool allowCommit) where K : BaseBusiness<K>, new();

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="func">
        /// 过滤条件
        /// </param>
        /// <param name="update">
        /// 创建匿名对象，包含字段用于更新
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Update<K>(Expression<Func<K, bool>> func, Expression<Func<K, K>> update) where K : BaseBusiness<K>, new();

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="item">
        /// 对象
        /// </param>
        /// <param name="commit">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Add<K>(K item, bool commit) where K : BaseBusiness<K>, new();

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="K">
        /// </typeparam>
        /// <param name="items">
        /// 对象
        /// </param>
        /// <param name="commit">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int AddRange<K>(IEnumerable<K> items, bool commit) where K : BaseBusiness<K>, new();

        /// <summary>
        /// 提交更新
        /// </summary>
        /// <returns></returns>
        int Commit();
    }
}