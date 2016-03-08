namespace TAF.Web.Businesses
{
    using System.Data.Entity;

    using TAF.MVC.Businesses;
    using TAF.Web.Models;

    public class WebDbContext : TAFDbContext
    {
        public DbSet<SystemDictionary> Dictionaries
        {
            get; set;
        }

        public DbSet<Product> Products
        {
            get; set;
        }

        public new static WebDbContext Create()
        {
            return new WebDbContext();
        }
    }
}