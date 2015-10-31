// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config.cs" company="">
//   
// </copyright>
// <summary>
//   配置
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    using System.Configuration;
    using System.Text;

    /// <summary>
    /// 配置
    /// </summary>
    public static class Config
    {
        #region DefaultEncoding(默认编码)

        /// <summary>
        /// 默认编码,值为utf-8
        /// </summary>
        public static Encoding DefaultEncoding
        {
            get
            {
                return Encoding.GetEncoding("utf-8");
            }
        }

        #endregion

        #region GetAppSettings(获取appSettings)

        /// <summary>
        /// 获取appSettings
        /// </summary>
        /// <param name="key">
        /// 键名
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        #endregion

        #region GetConnectionString(获取连接字符串)

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="key">
        /// 键名
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ToString();
        }

        #endregion

        #region GetProviderName(获取数据提供程序名称)

        /// <summary>
        /// 获取数据提供程序名称
        /// </summary>
        /// <param name="key">
        /// 键名
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetProviderName(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ProviderName;
        }

        #endregion
    }
}