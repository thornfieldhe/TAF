namespace TAF.MVC.Businesses
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class TAFDbContext : IdentityDbContext<ApplicationUser>
    {
        public TAFDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static TAFDbContext Create()
        {
            return new TAFDbContext();
        }
    }
}