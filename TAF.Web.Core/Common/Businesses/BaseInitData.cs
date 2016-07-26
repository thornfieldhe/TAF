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
    using TAF.Mvc.Businesses;
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
            CfgLoader.Instance.Load(AppDomain.CurrentDomain.BaseDirectory + "App_Data", "config.cfg");
            this.CustumerContextMigrate(Ioc.Create<IDbProvider>());
            DataSeeder.Instance.UpdateData();
        }



        /// <summary>
        /// 自定义数据迁移代码
        /// </summary>
        /// <param name="provider">
        /// The context.
        /// </param>
        protected virtual void CustumerContextMigrate(IDbProvider provider)
        {
            DataSeeder.Instance.OtherSeeders.Add(new AccountInitializer(provider, "AccountInit"));
        }
    }
}