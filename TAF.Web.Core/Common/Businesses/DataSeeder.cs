// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataSeeder.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   DataSeeder
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Businesses
{
    using System.Collections.Generic;

    using TAF.MVC.Interface;
    using TAF.Utility;

    /// <summary>
    /// 数据种子
    /// </summary>
    public class DataSeeder : SingletonBase<DataSeeder>
    {
        public DataSeeder()
        {
            OtherSeeders = new List<IContextSeeder>();
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