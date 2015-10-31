// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Serialization.cs" company="">
//   
// </copyright>
// <summary>
//   序列化扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    using System;
    using System.IO;
    using System.Runtime.Remoting.Messaging;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Xml.Serialization;

    /// <summary>
    /// The extensions.
    /// </summary>
    public partial class Extensions
    {
        /// <summary>
        /// The default formatter type.
        /// </summary>
        private const FormatterType DefaultFormatterType = FormatterType.Binary;

        /// <summary>
        /// 把对象序列化转换为字符串
        /// </summary>
        /// <param name="graph">
        /// </param>
        /// <param name="formatterType">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string SerializeObjectToString(this object graph, FormatterType formatterType)
        {
            using (var memoryStream = new MemoryStream())
            {
                var formatter = GetFormatter(formatterType);
                formatter.Serialize(memoryStream, graph);
                var arrGraph = memoryStream.ToArray();
                return Convert.ToBase64String(arrGraph);
            }
        }

        /// <summary>
        /// 把对象序列化转换为字符串
        /// </summary>
        /// <param name="graph">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string SerializeObjectToString(this object graph)
        {
            return SerializeObjectToString(graph, DefaultFormatterType);
        }

        /// <summary>
        /// 把已序列化为字符串类型的对象反序列化为指定的类型 
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="graph">
        /// </param>
        /// <param name="formatterType">
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T DeserializeStringToObject<T>(this string graph, FormatterType formatterType)
        {
            var arrGraph = Convert.FromBase64String(graph);
            using (var memoryStream = new MemoryStream(arrGraph))
            {
                var formatter = GetFormatter(formatterType);
                return (T)formatter.Deserialize(memoryStream);
            }
        }

        /// <summary>
        /// 把已序列化为字符串类型的对象反序列化为指定的类型
        /// </summary>
        /// <param name="graph">
        /// The graph.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T DeserializeStringToObject<T>(this string graph)
        {
            return DeserializeStringToObject<T>(graph, DefaultFormatterType);
        }

        /// <summary>
        /// 深度克隆
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="t">
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T DeepCopy<T>(this T t)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, t);
                stream.Seek(0, SeekOrigin.Begin);
                var copy = (T)formatter.Deserialize(stream);
                stream.Close();
                return copy;
            }
        }

        #region Xml序列化

        /// <summary>
        /// 序列化实例到Xml
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string XMLSerializer<T>(this T obj)
        {
            MemoryStream stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(typeof(T));
            try
            {
                // 序列化对象  
                xml.Serialize(stream, obj);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }

            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string str = sr.ReadToEnd();
            sr.Dispose();
            stream.Dispose();
            return str;
        }

        /// <summary>
        /// 反序列化Xml到实例
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="xml">
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T XMLDeserializeFromString<T>(this string xml)
        {
            try
            {
                if (xml != null)
                {
                    using (StringReader sr = new StringReader(xml))
                    {
                        XmlSerializer xmldes = new XmlSerializer(typeof(T));
                        return (T)xmldes.Deserialize(sr);
                    }
                }

                return default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        /// <summary>
        /// The get formatter.
        /// </summary>
        /// <param name="formatterType">
        /// The formatter Type.
        /// </param>
        /// 按照串行化的编码要求，生成对应的编码器
        /// <returns>
        /// The <see cref="IRemotingFormatter"/>.
        /// </returns>
        private static IRemotingFormatter GetFormatter(FormatterType formatterType)
        {
            switch (formatterType)
            {
                case FormatterType.Binary:
                    return new BinaryFormatter();
                case FormatterType.Soap:
                    return new SoapFormatter();
            }

            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// The formatter type.
    /// </summary>
    public enum FormatterType
    {
        /// <summary>
        /// The soap.
        /// </summary>
        Soap,

        /// <summary>
        /// The binary.
        /// </summary>
        Binary
    }
}