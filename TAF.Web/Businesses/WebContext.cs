namespace TAF.Web.Businesses
{
    using System.Data.Entity.Infrastructure;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class WebDbContext : TAFDbContext
    {
        public new static WebDbContext Create()
        {
            return new WebDbContext();
        }
    }
}