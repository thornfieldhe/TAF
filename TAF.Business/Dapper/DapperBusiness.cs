//namespace TAF
//{
//    using System;
//    using System.Data;
//    using System.Data.SqlClient;
//    using System.Linq;
//    using System.Linq.Expressions;
//
//    using TAF.Core;
//    using TAF.Data;
//    using TAF.Utility;
//
//    public class SqlService : SingletonBase<SqlService>
//    {
//        public string ConnectionString
//        {
//            get
//            {
//                return System.Configuration.ConfigurationManager.ConnectionStrings["CAFConnectionString"].ToString();
//            }
//        }
//
//        public SqlService()
//        {
//        }
//
//
//        public SqlConnection Connection
//        {
//            get
//            {
//                var connection = new SqlConnection(this.ConnectionString);
//                connection.Open();
//                return connection;
//            }
//        }
//    }
//
//    /// <summary>
//    /// 
//    /// </summary>
//    public class DapperBusiness<K> : BaseBusiness<K>, IDbAction where K : DapperBusiness<K>, IBusinessBase, new()
//    {
//        #region 常量定义
//
//        public static string TableName;
//        protected const string QUERY_COUNT = "SELECT COUNT(*) AS COUNT FROM {0} Where Status!=-1 {1}";
//        protected const string QUERY_GETBYID = "SELECT Top 1 * FROM {0} WHERE Id = @Id  AND Status!=-1";
//        protected const string QUERY_GETAll = "SELECT * FROM {0} WHERE  Status!=-1 {1} {2}";
//        protected const string QUERY_DELETE = "UPDATE {0} SET Status=-1 WHERE Id = @Id AND  Status!=-1";
//        //        protected const string QUERY_INSERT = "INSERT INTO Products ([Id], [Name], [CategoryId], [ColorId], [Price], [ProductionDate], [Status], [CreatedDate], [ChangedDate], [Note]) VALUES (@Id, @Name, @CategoryId, @ColorId, @Price, @ProductionDate, @Status, @CreatedDate, @ChangedDate, @Note)";
//        protected const string QUERY_EXISTS = "SELECT Count(*) FROM {0} WHERE Id = @Id AND Status!=-1";
//        //        protected const string QUERY_UPDATE = "UPDATE {0} SET {0} WHERE  Id = @Id  AND Version=@Version";
//
//        #endregion
//
//        #region 静态方法
//
//        public static T GetAll<T>() where T : CollectionBase<T, K>, new()
//        {
//            using (IDbConnection conn = SqlService.Instance.Connection)
//            {
//                var items = conn.Query<K>(string.Format(QUERY_GETAll, TableName, string.Empty, string.Empty), null).ToList();
//                var list = new T();
//                foreach (var item in items)
//                {
//                    item.Connection = SqlService.Instance.Connection;
//                    item.MarkOld();
//                    list.Add(item);
//                }
//                list.MarkOld();
//                return list;
//            }
//        }
//
//        /// <summary>
//        /// 表达式查询
//        /// </summary>
//        /// <param name="exp">表达式</param>
//        /// <returns></returns>
//        public static T Get<T>(Expression<Func<IQueryable<K>, IQueryable<K>>> exp) where T : CollectionBase<T, K>, new()
//        {
//            using (IDbConnection conn = SqlService.Instance.Connection)
//            {
//                var expc = new ExpConditions<K>();
//                expc.Add(exp);
//                var items = conn.Query<K>(string.Format(QUERY_GETAll, TableName, expc.Where(), expc.OrderBy())).ToList();
//
//                var list = new T();
//                foreach (var item in items)
//                {
//                    item.Connection = SqlService.Instance.Connection;
//                    item.MarkOld();
//                    list.Add(item);
//                }
//                list.MarkOld();
//                return list;
//            }
//        }
//
//        /// <summary>
//        /// 表达式查询
//        /// </summary>
//        /// <param name="exp">表达式</param>
//        /// <param name="conn"></param>
//        /// <param name="transaction"></param>
//        /// <returns></returns>
//        public static T Get<T>(Expression<Func<IQueryable<K>, IQueryable<K>>> exp,
//        IDbConnection conn, IDbTransaction transaction) where T : CollectionBase<T, K>, new()
//        {
//            var expc = new ExpConditions<K>();
//            expc.Add(exp);
//            var items = conn.Query<K>(string.Format(QUERY_GETAll, TableName, expc.Where(), expc.OrderBy()), null, transaction).ToList();
//
//            var list = new T();
//            foreach (var item in items)
//            {
//                item.Connection = SqlService.Instance.Connection;
//                item.MarkOld();
//                list.Add(item);
//            }
//            list.MarkOld();
//            return list;
//        }
//
//        public static K Find(Guid id)
//        {
//            using (IDbConnection conn = SqlService.Instance.Connection)
//            {
//                var item = conn.Query<K>(
//                    string.Format(QUERY_GETBYID, TableName), new
//                    {
//                        Id = id
//                    }).SingleOrDefault();
//                if (item == null)
//                {
//                    return null;
//                }
//                item.Connection = SqlService.Instance.Connection;
//                item.MarkOld();
//                return item;
//            }
//        }
//
//        /// <summary>
//        /// 是否存在
//        /// </summary>
//        /// <returns></returns>
//        public static bool Exists(Guid id)
//        {
//            using (IDbConnection conn = SqlService.Instance.Connection)
//            {
//                return conn.Query<int>(
//                    string.Format(QUERY_EXISTS, TableName), new
//                    {
//                        Id = id
//                    }).Single() >= 1;
//            }
//        }
//
//        /// <summary>
//        /// 数量查询
//        /// </summary>
//        /// <param name="exp">表达式</param>
//        /// <returns></returns>
//        public static int Count(Expression<Func<IQueryable<K>, IQueryable<K>>> exp)
//        {
//            using (IDbConnection conn = SqlService.Instance.Connection)
//            {
//                var expc = new ExpConditions<K>();
//                expc.Add(exp);
//                return conn.Query<int>(string.Format(QUERY_COUNT, TableName, expc.Where())).Single();
//            }
//        }
//
//        /// <summary>
//        /// 直接删除
//        /// </summary>
//        /// <returns></returns>
//        public static int Delete(Guid id)
//        {
//            using (IDbConnection conn = SqlService.Instance.Connection)
//            {
//                return conn.Execute(string.Format(QUERY_DELETE, TableName), new
//                {
//                    Id = id
//                });
//            }
//        }
//
//        #endregion
//
//        [NonSerialized]
//        protected IDbConnection _connection;
//
//        protected int _changedRows = 0;//影响行
//        protected string _updateParameters = "";// 用于拼接更新方法所需字段
//
//        //        public string TableName
//        //        {
//        //            get; protected set;
//        //        }
//
//        public string[] SkipedProperties
//        {
//            get; private set;
//        }
//
//        public IDbConnection Connection
//        {
//            get
//            {
//                return this._connection;
//            }
//            set
//            {
//                this._connection = value;
//
//            }
//        }
//
//        /// <summary>
//        /// true：只更新关系
//        /// false：标记删除
//        /// </summary>
//        public bool IsChangeRelationship
//        {
//            get; set;
//        }
//
//
//
//
//
//        #region  数据库操作方法
//
//        /// <summary>
//        /// 插入或更新中过滤不需要的字段，
//        /// properties：A,B,C
//        /// </summary>
//        /// <param name="properties"></param>
//        public void SkipProperties(string properties)
//        {
//            var propertyList = properties.Split(',');
//            this.SkipedProperties = propertyList;
//            foreach (var property in propertyList)
//            {
//                this._updateParameters = this._updateParameters.Replace(string.Format(", {0} =  @{0}", property), "");
//            }
//
//        }
//
//        public virtual int Create()
//        {
//            this._changedRows = 0;
//            using (IDbConnection conn = this.Connection)
//            {
//                var transaction = conn.BeginTransaction();
//                try
//                {
//                    this.PreInsert(conn, transaction);
//                    this.Validate();
//                    this._changedRows += this.Insert(conn, transaction);
//                    this.PostInsert(conn, transaction);
//                    transaction.Commit();
//                    this.MarkOld();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    throw ex;
//                }
//            }
//            return this._changedRows;
//        }
//
//        public virtual int Save()
//        {
//            this._changedRows = 0;
//            using (IDbConnection conn = this.Connection)
//            {
//                var transaction = conn.BeginTransaction();
//                try
//                {
//                    this.PreUpdate(conn, transaction);
//                    if (this.IsDirty)
//                    {
//                        this.Validate();
//                        this._changedRows += this.Update(conn, transaction);
//                        this.PostUpdate(conn, transaction);
//                        transaction.Commit();
//                        this.MarkOld();
//                    }
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    throw ex;
//                }
//            }
//            return this._changedRows;
//        }
//
//        public virtual int Delete()
//        {
//            using (IDbConnection conn = this.Connection)
//            {
//                this._changedRows = 0;
//                var transaction = conn.BeginTransaction();
//                try
//                {
//                    this.PreDelete(conn, transaction);
//                    this._changedRows += this.Delete(conn, transaction);
//                    this.PostDelete(conn, transaction);
//                    transaction.Commit();
//                    this.MarkDelete();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    throw ex;
//                }
//            }
//            return this._changedRows;
//        }
//
//        /// <summary>
//        /// 适用于子元素更新
//        /// 和不需要知道元素状态的更新
//        /// </summary>
//        /// <returns></returns>
//        public virtual int SubmitChange()
//        {
//            this._changedRows = 0;
//            using (IDbConnection conn = this.Connection)
//            {
//                var transaction = conn.BeginTransaction();
//                try
//                {
//                    this._changedRows += this.SaveChange(conn, transaction);
//                    transaction.Commit();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    throw ex;
//                }
//                return this._changedRows;
//            }
//        }
//
//        /// <summary>
//        /// 适用于子元素更新
//        /// 和不需要知道元素状态的更新
//        /// </summary>
//        /// <returns></returns>
//        public virtual int SaveChange(IDbConnection conn, IDbTransaction transaction)
//        {
//            if (this.IsDelete && !this.IsChangeRelationship)
//            {
//                this.PreDelete(conn, transaction);
//                this._changedRows += this.Delete(conn, transaction);
//                this.PostDelete(conn, transaction);
//                this.MarkOld();
//                return this._changedRows;
//            }
//            else if (this.IsNew)
//            {
//                this.Validate();
//                this.PreInsert(conn, transaction);
//                this.Insert(conn, transaction);
//                this.PostInsert(conn, transaction);
//                this.MarkOld();
//                return this._changedRows;
//            }
//            else if (this.IsDirty)
//            {
//                this.Validate();
//                this.PreUpdate(conn, transaction);
//                this._changedRows += this.Update(conn, transaction);
//                this.PostUpdate(conn, transaction);
//                this.MarkOld();
//            }
//            return this._changedRows;
//        }
//
//        public virtual int Update(IDbConnection conn, IDbTransaction transaction)
//        {
//            return 0;
//        }
//
//        public virtual int Insert(IDbConnection conn, IDbTransaction transaction)
//        {
//            return 0;
//        }
//
//        public virtual int Delete(IDbConnection conn, IDbTransaction transaction)
//        {
//            return 0;
//        }
//
//        protected virtual void PreFetch(IDbConnection conn)
//        {
//        }
//
//        protected virtual void PreInsert(IDbConnection conn, IDbTransaction transaction)
//        {
//        }
//
//        protected virtual void PreUpdate(IDbConnection conn, IDbTransaction transaction)
//        {
//        }
//
//        protected virtual void PreDelete(IDbConnection conn, IDbTransaction transaction)
//        {
//        }
//
//        protected virtual void PostFetch(IDbConnection conn)
//        {
//        }
//
//        protected virtual void PostUpdate(IDbConnection conn, IDbTransaction transaction)
//        {
//        }
//
//        protected virtual void PostDelete(IDbConnection conn, IDbTransaction transaction)
//        {
//        }
//
//        protected virtual void PostInsert(IDbConnection conn, IDbTransaction transaction)
//        {
//        }
//
//        #endregion
//    }
//}