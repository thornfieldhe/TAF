// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MigrateDatabase.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   MigrateDatabase
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.MVC.Common.Businesses
{

    using TAF.Mvc.Business;
    using TAF.Mvc.Model;

    /// <summary>
    /// 数据库迁移基类
    /// </summary>
    public abstract class MigrateDatabase
    {
        public void AccountContextMigrate()
        {
            using (var context = new AccountContext())
            {
                context.OtherSeeders.Add(new AccountDbInitializer(context, "AccountInit"));
                MigrateOtherData(context);
                context.UpdateData();
            }
        }

        protected abstract void MigrateOtherData(AccountContext context);
    }
}