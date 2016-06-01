namespace TAF.Web.Businesses
{
    using System.Data.Entity;

    using TAF.Mvc.Businesses;
    using TAF.Web.Models;

    public class WebDbContext : AccountDbContext
    {
        public DbSet<SystemDictionary> Dictionaries
        {
            get; set;
        }

        public DbSet<Product> Products
        {
            get; set;
        }

        public DbSet<Article> Articles
        {
            get; set;
        }

        public new static WebDbContext Create()
        {
            return new WebDbContext();
        }
    }
}