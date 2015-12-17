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
    public class EfBusiness<K> : BaseBusiness<K>, IDbAction where K : EfBusiness<K>, IBusinessBase, new()
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
        /// EF数据库对象
        /// </summary>
        [NotMapped]
        protected DbContext DbContex
        {
            get; set;
        }

        public override bool IsClean { get { return DbContex.Entry<K>(this as K).State == EntityState.Unchanged; } }

        #region 静态方法

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
        public static K Find(Guid id)
        {
            return new K().QuerySingle(i => i.Id == id);
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

        public static void Delete(Expression<Func<K, bool>> func)
        {
            Ioc.Create<IContextWapper>().Context.Set<K>().Where(func).Delete();
        }

        public static void Update(Expression<Func<K, bool>> func, Expression<Func<K, K>> update)
        {
            Ioc.Create<IContextWapper>().Context.Set<K>().Where(func).Update(update);
        }

        #endregion

        #region 实例方法
        /// <summary>
        /// 初始化对象
        /// </summary>
        public virtual void Init()
        {
        }

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
        /// 提交一个对象
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Commit()
        {
            var result = 0;
            PreSubmit();
            Validate();
            result += Submit();
            result += PostSubmit();
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

        /// <summary>
        /// 查询一个对象
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        public K Get(Guid id)
        {
            return QuerySingle(i => i.Id == id);
        }

        /// <summary>
        /// 查询对象列表
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// </returns>
        public List<K> Query(Expression<Func<K, bool>> func, bool useCache = false)
        {
            var query = DbContex.Set<K>().Where(func);
            var items = PreQuery(query, useCache);
            return PostQuery(items);
        }

        /// <summary>
        /// 条件查询一个对象列表
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        public K QuerySingle(Expression<Func<K, bool>> func)
        {
            var query = DbContex.Set<K>().Where(func);
            var item = PreQuerySingle(query);
            return PostQuerySingle(item);
        }

        #region 继承方法

        #region 插入

        /// <summary>
        /// 在插入之前给对象赋值
        /// </summary>
        protected virtual void PreInsert()
        {
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
            DbContex.Set<K>().Add(this as K);
            return DbContex.SaveChanges();
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
            return !IsClean ? this.DbContex.SaveChanges() : 0;
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

        #region 提交 （包含插入和更新操作） 

        /// <summary>
        /// 在提交之前给对象赋值
        /// </summary>
        protected virtual void PreSubmit()
        {

        }

        /// <summary>
        /// 提交对象到数据库
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int Submit()
        {
            return this.IsNew ? this.Insert() : this.Update();
        }

        /// <summary>
        /// 在提交之后操作
        /// </summary>
        /// <returns>
        /// </returns>
        protected virtual int PostSubmit()
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
            //            DbContex.SaveChanges();
            //硬删除
            DbContex.Set<K>().Remove(this as K);
            return this.DbContex.SaveChanges();
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

        #region 查询

        /// <summary>
        /// 执行查询前，修改查询条件
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// </returns>
        protected virtual K PreQuerySingle(IQueryable<K> query)
        {
            return query.FirstOrDefault();
        }

        /// <summary>
        /// 查询完成后给查询结果属性赋值
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// </returns>
        protected virtual K PostQuerySingle(K item)
        {
            item.IfNotNull(i => i.DbContex = DbContex);
            item.MarkOld();
            return item;
        }

        /// <summary>
        /// 查询之前，判断是否是从缓存中取数
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// </returns>
        protected virtual List<K> PreQuery(IQueryable<K> query, bool useCache = false)
        {
            var items = useCache
                            ? query.FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(60))).ToList()
                            : query.ToList();
            return PostQuery(items);
        }

        /// <summary>
        /// 查询所有对象列表
        /// </summary>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// </returns>
        protected List<K> Query(bool useCache = false)
        {
            var query = DbContex.Set<K>();
            var items = PreQuery(query, useCache);
            return PostQuery(items);
        }

        /// <summary>
        /// 查询完成后给查询结果属性赋值
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <returns>
        /// </returns>
        protected virtual List<K> PostQuery(List<K> items)
        {
            items.ForEach(
                i =>
                    {
                        i.DbContex = DbContex;
                        i.MarkOld();
                    });
            return items;
        }
        #endregion

        #endregion

        #endregion
    }
}
