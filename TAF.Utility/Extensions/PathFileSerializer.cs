// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PathFileSerializer.cs" company="">
//   
// </copyright>
// <summary>
//   序列化对象到文件
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TAF.Utility
{
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// 序列化对象到文件
    /// </summary>
    public class PathFileSerializer
    {
        /// <summary>
        /// 对象序列化成 XML 
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Serialize<T>(T obj, string path)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                path = path.Replace('\\', '/');
                string dir = string.Empty;
                if (path.LastIndexOf('/') != -1)
                {
                    dir = path.Substring(0, path.LastIndexOf('/'));
                }

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                FileStream stream = File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                xmlSerializer.Serialize(stream, obj);
                stream.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// XML 反序列化成对象
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// </returns>
        public static T Deserialize<T>(string path)
        {
            T t = default(T);
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                if (!File.Exists(path))
                {
                    return default(T);
                }

                FileStream stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                object obj = xmlSerializer.Deserialize(stream);
                t = (T)obj;
                stream.Close();
            }
            catch
            {
                return default(T);
            }

            return t;
        }
    }
}