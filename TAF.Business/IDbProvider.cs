namespace TAF
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// 数据库提供者接口
    /// </summary>
    /// <typeparam name="K">
    /// 泛型对象
    /// </typeparam>
    public interface IDbProvider<K>
        where K : new()
    {
        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="query">
        /// 过滤条件
        /// </param>
        /// <param name="useCache">
        /// 是否从缓存中查询
        /// </param>
        /// <returns>
        /// </returns>
        List<K> Get(Expression<Func<K, bool>> query, bool useCache = false);

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="func">过滤条件</param>
        /// <param name="useCache">是否从缓存中查询</param>
        /// <returns></returns>
        K Find(Expression<Func<K, bool>> func, bool useCache = false);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="R">排序对象</typeparam>
        /// <typeparam name="T">结果转换为T的列表输出</typeparam>
        /// <param name="pager">
        /// 分页对象
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
        /// 是否从缓存中读取
        /// </param>
        /// <returns></returns>
        Pager<T> Pages<R, T>(
            Pager<T> pager,
            Func<K, bool> whereFunc,
            Func<K, R> orderByFunc,
            bool isAsc = true,
            bool useCache = false) where T : new();

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="func">过滤条件</param>
        /// <returns></returns>
        bool Exist(Expression<Func<K, bool>> func);

        /// <summary>
        /// 查询数据条数
        /// </summary>
        /// <param name="func">过滤条件</param>
        /// <returns></returns>
        int Count(Expression<Func<K, bool>> func);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Delete(Guid id);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="func">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Delete(Expression<Func<K, bool>> func);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="func">
        /// 过滤条件
        /// </param>
        /// <param name="update">
        /// 创建匿名对象，包含字段用于更新
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Update(Expression<Func<K, bool>> func, Expression<Func<K, K>> update);

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="item">
        /// 对象
        /// </param>
        /// <param name="commit"></param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Add(K item, bool commit);

        /// <summary>
        /// 提交更新
        /// </summary>
        /// <returns></returns>
        int Commit();
    }
}