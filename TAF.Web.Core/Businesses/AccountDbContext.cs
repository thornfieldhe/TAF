namespace TAF.Mvc.Businesses
{
    using System.Data.Entity;

    using EntityFramework.DynamicFilters;

    using Microsoft.AspNet.Identity.EntityFramework;

    using TAF.Core;
    using TAF.Mvc.Model;

    public class AccountDbContext : IdentityDbContext<ApplicationUser>
    {
        public AccountDbContext()
            : base("AccountConnection", throwIfV1Schema: false)
        {
        }

        private DbSet<UpdateMigration> UpdateMigrations
        {
            get; set;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("Status", (IBusinessBase b, int status) => (b.Status != status), () => -1);
            base.OnModelCreating(modelBuilder);
        }

        public static AccountDbContext Create()
        {
            return new AccountDbContext();
        }
    }
}