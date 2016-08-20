namespace TAF.Mvc.Business
{
    using System.Data.Entity;

    using EntityFramework.DynamicFilters;

    using TAF.Core;
    using TAF.Mvc.Model;


    /// <summary>
    /// 
    /// </summary>
    public class TAFContext : DbContext
    {
        public TAFContext() : this("main")
        {
        }

        public TAFContext(string connection) : base(connection)
        {
        }

        public static TAFContext Create()
        {
            return new TAFContext();
        }


        public DbSet<TAF.Mvc.Model.Log> Logs
        {
            get; set;
        }

        public DbSet<UpdateMigration> UpdateMigrations
        {
            get; set;
        }

        public DbSet<User> Users
        {
            get; set;
        }

        public DbSet<Role> Roles
        {
            get; set;
        }

        public DbSet<UserRole> UserRoles
        {
            get; set;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("Status", (IBusinessBase b, int status) => (b.Status != status), () => -1);
            base.OnModelCreating(modelBuilder);
        }
    }
}