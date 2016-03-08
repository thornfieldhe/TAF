namespace TAF.Web.Models
{
    using System;

    using TAF.Core;

    /// <summary>
    /// 
    /// </summary>
    public class ProductItemView : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public Guid CategoryId
        {
            get; set;
        }

        public Guid? ColorId
        {
            get; set;
        }

        public decimal Price
        {
            get; set;
        }

        public string ProductionDate
        {
            get; set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ProductListView : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }


        public string Category
        {
            get; set;
        }

        public string Color
        {
            get; set;
        }

        public decimal Price
        {
            get; set;
        }

        public string ProductionDate
        {
            get; set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ProductQueryView
    {

        public string Name
        {
            get; set;
        }

        public Guid CategoryId
        {
            get; set;
        }

        public Guid? ColorId
        {
            get; set;
        }

        public decimal Price
        {
            get; set;
        }

        public DateTime ProductionDateFrom
        {
            get; set;
        }

        public DateTime ProductionDateTo
        {
            get; set;
        }
    }
}