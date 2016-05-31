namespace TAF.MVC.Business
{
    using System.Collections.Generic;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using TAF.Mvc.Model;
    using TAF.MVC.Interface;


    /// <summary>
    /// 
    /// </summary>
    public class AccountContext : IdentityDbContext<ApplicationUser>
    {
        public AccountContext() : base("AccountConnection", false)
        {
            OtherSeeders = new List<IContextSeeder>();
        }

        public static AccountContext Create()
        {
            return new AccountContext();
        }


        //        public DbSet<Log> Logs
        //        {
        //            get; set;
        //        }

        public DbSet<UpdateMigration> UpdateMigrations
        {
            get; set;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");
            //            DbInterception.Add(new FilterInterceptor());
        }


        /// <summary>
        /// 设置其他数据初始化器
        /// </summary>
        public IList<IContextSeeder> OtherSeeders
        {
            get; set;
        }

        public void UpdateData()
        {
            foreach (var seeder in this.OtherSeeders)
            {
                seeder.Update();
            }
        }
    }
}