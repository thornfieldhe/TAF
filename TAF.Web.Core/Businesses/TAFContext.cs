namespace TAF.Mvc.Businesses
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Interception;

    using Microsoft.AspNet.Identity.EntityFramework;

    using TAF.Mvc.Model;

    public class TAFDbContext : IdentityDbContext<ApplicationUser>
    {
        public TAFDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
//            DbInterception.Add(new FilterInterceptor());
//            modelBuilder.Configurations.Add(new UserMap());
            base.OnModelCreating(modelBuilder);
        }

        public static TAFDbContext Create()
        {
            return new TAFDbContext();
        }
    }
}