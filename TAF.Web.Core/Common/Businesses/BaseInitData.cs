// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseInitData.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   BaseInitData
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.MVC.Common.Businesses
{
    using System;
    using System.Data.Entity;

    using TAF.Mvc.Business;
    using TAF.Mvc.Model;
    using TAF.Utility;

    /// <summary>
    /// 数据初始化基类
    /// </summary>
    public class BaseInitData : SingletonBase<BaseInitData>
    {
        /// <summary>
        /// 数据迁移
        /// </summary>
        public void DatabaseMigrate()
        {
            using (var context = new TAFContext())
            {
                CfgLoader.Instance.Load(AppDomain.CurrentDomain.BaseDirectory + "App_Data", "config.cfg");
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<TAFContext, DefaultConfiguration>());
                this.CustumerContextMigrate(context);
                context.UpdateData();
            }
        }



        /// <summary>
        /// 自定义数据迁移代码
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected virtual void CustumerContextMigrate(TAFContext context)
        {
            context.OtherSeeders.Add(new AccountDbInitializer(context, "AccountInit"));
        }
    }
}