namespace TAF.Utility
{
    using System;

    public class ConfigManager : SingletonBase<ConfigManager>
    {
        public SharpConfig.Configuration Cfg
        {
            get; set;
        }
        /// <summary>
        /// 获取配置文件配置
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="name">名</param>
        /// <returns>
        /// 值
        /// </returns>
        public K GetConfig<K>(string nameSpace, string name = null) where K : class
        {
            if (Cfg == null)
            {
                var file = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\config.ini";
                Cfg = SharpConfig.Configuration.LoadFromFile(file);
            }

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(nameSpace))
            {
                return Cfg[nameSpace][name].GetValueTyped<K>();
            }
            else if (!string.IsNullOrWhiteSpace(nameSpace))
            {
                return Cfg[nameSpace].CreateObject<K>();
            }
            throw new Exception("配置文件不存在");
        }

        /// <summary>
        /// 配置文件存储值
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="nameSpace"></param>
        /// <param name="name"></param>
        public void SaveConfig<K>(K value, string nameSpace, string name) where K : class
        {
            Cfg[nameSpace][name].SetValue(value);
        }
    }
}