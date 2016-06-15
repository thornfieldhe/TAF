// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CfgLoader.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   CfgLoader
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Model
{
    using System;
    using System.IO;
    using System.Linq;

    using SharpConfig;

    /// <summary>
    /// 自定义配置加载器
    /// </summary>
    public class CfgLoader : BaseLoader<CfgLoader, Configuration>
    {
        protected override void LoadItem()
        {
            var file = Directory.GetFiles(this.ItemPath, this.ItemFile).First();
            this.Item = SharpConfig.Configuration.LoadFromFile(file);
        }

        public P GetConfig<P>(string nameSpace, string name = null)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(nameSpace))
            {
                return Item[nameSpace][name].GetValue<P>();
            }
            if (!string.IsNullOrWhiteSpace(nameSpace))
            {
                return this.Item[nameSpace].CreateObject<P>();
            }

            throw new Exception("配置不存在");
        }

        /// <summary>
        /// 获取配置文件配置
        /// </summary>
        /// <typeparam name="P">
        /// </typeparam>
        /// <param name="nameSpace">
        /// 命名空间
        /// </param>
        /// <param name="name">
        /// 名
        /// </param>
        /// <returns>
        /// 值
        /// </returns>
        public P[] GetArraryConfig<P>(string nameSpace, string name)
        {

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(nameSpace))
            {
                return Item[nameSpace][name].GetValueArray<P>();
            }

            throw new Exception("配置不存在");
        }
    }
}