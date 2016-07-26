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
    using System.Linq;
    using TAF.MVC.Interface;

    /// <summary>
    /// 业务数据库上下文扩展基类
    /// </summary>
    public abstract class CommonContextSeeder : IContextSeeder
    {
        protected CommonContextSeeder(IDbProvider provider, string key)
        {
            this.Provider = provider;
            this.Key = key;
        }

        public string Key
        {
            get; protected set;
        }

        public IDbProvider Provider
        {
            get; protected set;
        }

        public abstract void UpdateData();

        public void Update()
        {
            if (UpdateMigration.Exist(r => r.Key == this.Key.Trim().ToLower()))
            {
                UpdateData();
                UpdateMigration.Add(new UpdateMigration { Key = this.Key });
            }
        }
    }
}