using System.Data.Entity;

namespace TAF.BusinessEntity.Test
{
    using System.Data.Entity.Infrastructure.Interception;

    using EntityFramework.Filters;

    public class TestDbContext : DbContext
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DbInterception.Add(new FilterInterceptor());
            modelBuilder.Configurations.Add(new UserMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}