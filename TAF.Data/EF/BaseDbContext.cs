namespace TAF.Data
{
    using System.Data.Entity;

    using EntityFramework.DynamicFilters;

    using TAF.Core;

    /// <summary>
    /// 数据库上下文基类
    /// </summary>
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(string connectiion)
            : base(connectiion)
        {
        }

        public BaseDbContext() : this("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("Status", (IBusinessBase b, int status) => (b.Status != status), () => -1);
            base.OnModelCreating(modelBuilder);
        }
    }
}