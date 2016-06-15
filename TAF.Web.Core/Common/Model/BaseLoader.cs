// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseLoader.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   BaseLoader
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Model
{
    using System.IO;
    using System.Linq;

    using TAF.Utility;

    /// <summary>
    /// 配置文件加载基类
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="K">
    /// </typeparam>
    public abstract class BaseLoader<T, K> : SingletonBase<T> where T : new()
    {
        public string ItemPath
        {
            get; protected set;
        }

        public string ItemFile
        {
            get; protected set;
        }

        public K Item
        {
            get; protected set;
        }

        /// <summary>
        /// 加载系统选项和用户个人创建的选项
        /// </summary>
        /// <param name="path">
        /// 加载路径
        /// </param>
        /// <param name="files">
        /// 加载文件
        /// </param>
        public void Load(string path, string files)
        {
            this.ItemPath = path;
            this.ItemFile = files;
            this.LoadItem();
        }


        /// <summary>
        /// 系统载入选择项
        /// </summary>
        protected virtual void LoadItem()
        {
            foreach (var infos in Directory.GetFiles(this.ItemPath, this.ItemFile).Select(PathFileSerializer.JsonDeSerialize<K>).Where(infos => infos != null))
            {
                this.Item = infos;
            }
        }

        /// <summary>
        /// 重新加载数据
        /// </summary>
        public void Reload()
        {
            Load(this.ItemPath, this.ItemFile);
        }
    }
}