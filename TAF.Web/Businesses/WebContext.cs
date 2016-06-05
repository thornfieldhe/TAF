namespace TAF.Web.Businesses
{
    using System.Data.Entity;

    using TAF.Mvc.Business;
    using TAF.Web.Models;

    public class WebDbContext : AccountContext
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