using System.Data.Entity;

namespace TAF.BusinessEntity.Test
{

    using EntityFramework.DynamicFilters;

    using TAF.Core;
    using TAF.Data;

    public class TestDbContext : BaseDbContext
    {
        public TestDbContext(string connectiion)
            : base(connectiion)
        {
        }

        public TestDbContext() : this("DefaultConnection")
        {
        }

        public DbSet<Student> Students
        {
            get; set;
        }

        public DbSet<User> Users
        {
            get; set;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}