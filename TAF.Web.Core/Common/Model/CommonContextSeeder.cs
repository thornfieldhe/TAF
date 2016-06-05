// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonContextSeeder.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   CommonContextSeeder
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Model
{
    using System.Data.Entity;
    using System.Linq;
    using TAF.MVC.Interface;

    /// <summary>
    /// 业务数据库上下文扩展基类
    /// </summary>
    public abstract class CommonContextSeeder : IContextSeeder
    {
        protected CommonContextSeeder(DbContext context, string key)
        {
            this.Context = context;
            this.Key = key;
        }

        public string Key
        {
            get; protected set;
        }

        public DbContext Context
        {
            get; protected set;
        }

        public abstract void UpdateData();

        public void Update()
        {
            if (!this.Context.Set<UpdateMigration>().Any(r => r.Key == this.Key.Trim().ToLower()))
            {
                UpdateData();
                this.Context.Set<UpdateMigration>().Add(new UpdateMigration { Key = this.Key });
                this.Context.SaveChanges();
            }
        }
    }
}