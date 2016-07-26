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
        public TAFContext() : base("main")
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("Status", (IBusinessBase b, int status) => (b.Status != status), () => -1);
            base.OnModelCreating(modelBuilder);
        }
    }
}