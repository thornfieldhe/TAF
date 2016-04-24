using System;

namespace TAF.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.SqlClient;
    using TAF.Core;
    using TAF.Data;
    using TAF.Utility;

    public class SqlService : SingletonBase<SqlService>
    {
        public string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["CAFConnectionString"].ToString();
            }
        }

        public SqlService()
        {
        }


        public SqlConnection Connection
        {
            get
            {
                var connection = new SqlConnection(this.ConnectionString);
                connection.Open();
                return connection;
            }
        }
    }

    [Serializable]
    public partial class Product : DapperBusiness<Product>, IEntityBase
    {
        public Product()
        {
            this.Connection = SqlService.Instance.Connection;
            TableName = "Products";
        }

        public new static string TableName = "Products";
        #region 公共属性

        private string _name = String.Empty;
        private Guid _categoryId = Guid.Empty;
        private Guid? _colorId = Guid.Empty;
        private decimal _price;
        private DateTime _productionDate;

        [Required(ErrorMessage = @"Name不允许为空")]
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this.SetProperty(ref this._name, value);
            }
        }

        [GuidRequired(ErrorMessage = @"Category不允许为空")]
        public Guid CategoryId
        {
            get
            {
                return this._categoryId;
            }
            set
            {
                this.SetProperty(ref this._categoryId, value);
            }
        }

        public Guid? ColorId
        {
            get
            {
                return this._colorId;
            }
            set
            {
                this.SetProperty(ref this._colorId, value);
            }
        }



        [Required(ErrorMessage = @"Price不允许为空")]
        public decimal Price
        {
            get
            {
                return this._price;
            }
            set
            {
                this.SetProperty(ref this._price, value);
            }
        }

        [Required(ErrorMessage = @"ProductionDate不允许为空")]
        [DateTimeRequired(ErrorMessage = @"ProductionDate不允许为空")]
        public DateTime ProductionDate
        {
            get
            {
                return this._productionDate;
            }
            set
            {
                this.SetProperty(ref this._productionDate, value);
            }
        }

        #endregion

        //        #region 常量定义
        //        protected const string QUERY_COUNT = "SELECT COUNT(*) AS COUNT FROM Products Where Status!=-1 ";
        //        const string QUERY_GETBYID = "SELECT Top 1 * FROM Products WHERE Id = @Id  AND Status!=-1";
        //        const string QUERY_GETAll = "SELECT * FROM Products WHERE  Status!=-1";
        //        const string QUERY_DELETE = "UPDATE Products SET Status=-1 WHERE Id = @Id AND  Status!=-1";
        //        const string QUERY_EXISTS = "SELECT Count(*) FROM Products WHERE Id = @Id AND Status!=-1";
        //        const string QUERY_INSERT = "INSERT INTO Products ([Id], [Name], [CategoryId], [ColorId], [Price], [ProductionDate], [Status], [CreatedDate], [ChangedDate], [Note]) VALUES (@Id, @Name, @CategoryId, @ColorId, @Price, @ProductionDate, @Status, @CreatedDate, @ChangedDate, @Note)";
        //        const string QUERY_UPDATE = "UPDATE {0} SET {0} WHERE  Id = @Id  AND Version=@Version";
        //        #endregion




        public override int Delete(IDbConnection conn, IDbTransaction transaction)
        {
            base.MarkDelete();
            return conn.Execute(QUERY_DELETE, new
            {
                Id = this.Id
            }, transaction, null, null);
        }

        public override int Update(IDbConnection conn, IDbTransaction transaction)
        {
            if (!this.IsDirty)
            {
                return this._changedRows;
            }
//            this._updateParameters += ", ChangedDate = GetDate()";
//            var query = String.Format(QUERY_UPDATE, this.TableName, this._updateParameters.TrimStart(','));
//            this._changedRows += conn.Execute(query, this, transaction, null, null);
            return this._changedRows;
        }

        public override int Insert(IDbConnection conn, IDbTransaction transaction)
        {
//            this._changedRows += conn.Execute(QUERY_INSERT, this, transaction, null, null);
            return this._changedRows;
        }

        #region 私有方法


        /// <summary>
        /// 添加描述
        /// </summary>
        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            this.AddDescription("Name:" + this.Name + ",");
            this.AddDescription("CategoryId:" + this.CategoryId + ",");
            this.AddDescription("ColorId:" + this.ColorId + ",");
            this.AddDescription("Price:" + this.Price + ",");
            this.AddDescription("ProductionDate:" + this.ProductionDate + ",");
        }
        #endregion

    }

    [Serializable]
    public class ProductList : CollectionBase<ProductList, Product>
    {
        public ProductList()
        {
            this.Connection = SqlService.Instance.Connection;
            this.TableName = "Products";
        }
    }
}


