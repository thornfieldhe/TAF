// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reflection.cs" company="">
//   
// </copyright>
// <summary>
//   反射操作
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;
    using Files;
    using Utility;

    /// <summary>
    /// 反射操作
    /// </summary>
    public static class Reflection
    {
        #region GetType(获取类型)

        /// <summary>
        /// 获取类型,对可空类型进行处理
        /// </summary>
        /// <typeparam name="T">
        /// 类型
        /// </typeparam>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        public static Type GetType<T>()
        {
            return Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        }

        #endregion

        #region CreateInstance(动态创建实例)

        /// <summary>
        /// 动态创建实例
        /// </summary>
        /// <typeparam name="T">
        /// 目标类型
        /// </typeparam>
        /// <param name="className">
        /// 类名，包括命名空间,如果类型不处于当前执行程序集中，需要包含程序集名，范例：Test.Core.Test2,Test.Core
        /// </param>
        /// <param name="parameters">
        /// 传递给构造函数的参数
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T CreateInstance<T>(string className, params object[] parameters)
        {
            var type = Type.GetType(className) ?? Assembly.GetCallingAssembly().GetType(className);
            return CreateInstance<T>(type, parameters);
        }

        /// <summary>
        /// 动态创建实例
        /// </summary>
        /// <typeparam name="T">
        /// 目标类型
        /// </typeparam>
        /// <param name="type">
        /// 类型
        /// </param>
        /// <param name="parameters">
        /// 传递给构造函数的参数
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T CreateInstance<T>(Type type, params object[] parameters)
        {
            return Activator.CreateInstance(type, parameters).To<T>();
        }

        #endregion

        #region GetByInterface(获取实现了接口的所有具体类型)

        /// <summary>
        /// 获取实现了接口的所有具体类型
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="assembly">在该程序集中查找</param>
        public static List<T> GetByInterface<T>(Assembly assembly)
        {
            var typeInterface = typeof(T);
            return assembly.GetTypes()
                .Where(t => typeInterface.IsAssignableFrom(t) && t != typeInterface && t.IsAbstract == false)
                .Select(t => CreateInstance<T>(t)).ToList();
        }

        #endregion

        #region GetAssemblies(从目录中获取所有程序集)

        /// <summary>
        /// 从目录中获取所有程序集
        /// </summary>
        /// <param name="directoryPath">目录绝对路径</param>
        public static List<Assembly> GetAssemblies(string directoryPath)
        {
            var filePaths = File.GetAllFiles(directoryPath).Where(t => t.EndsWith(".exe") || t.EndsWith(".dll"));
            return filePaths.Select(Assembly.LoadFile).ToList();
        }

        #endregion

        #region 获取Attribute属性

        /// <summary>
        /// 获取某个类型包括指定属性的集合
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="type">
        /// </param>
        /// <returns>
        /// </returns>
        internal static IList<T> GetCustomAttributes<T>(Type type) where T : Attribute
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var attributes = (T[])type.GetCustomAttributes(typeof(T), false);
            return (attributes.Length == 0) ? new List<T>() : new List<T>(attributes);
        }





        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetTableName<T>()
        {
            var attribute = GetCustomAttributes<TableAttribute>(typeof(T)).FirstOrDefault();

            return attribute != null ? attribute.Name : String.Empty;
        }

        #region GetMemberDescription(获取描述)

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <typeparam name="T">
        /// 类型
        /// </typeparam>
        /// <param name="memberName">
        /// 成员名称
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetMemberDescription<T>(string memberName)
        {
            return GetMemberDescription(GetType<T>(), memberName);
        }

        public static string GetFieldDescription<T>(string fieldName)
        {
            return GetFiledDescription(GetType<T>(), fieldName);
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="type">
        /// 类型
        /// </param>
        /// <param name="memberName">
        /// 成员名称
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetMemberDescription(Type type, string memberName)
        {
            if (type == null)
            {
                return String.Empty;
            }

            if (String.IsNullOrWhiteSpace(memberName))
            {
                return String.Empty;
            }

            return GetMemberDescription(type, type.GetProperty(memberName));
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="type">
        /// 类型
        /// </param>
        /// <param name="filedName">
        /// 成员名称
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetFiledDescription(Type type, string filedName)
        {
            if (type == null)
            {
                return String.Empty;
            }

            if (String.IsNullOrWhiteSpace(filedName))
            {
                return String.Empty;
            }

            return GetMemberDescription(type, type.GetField(filedName));
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="type">
        /// 类型
        /// </param>
        /// <param name="field">
        /// 成员
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetMemberDescription(Type type, FieldInfo field)
        {
            if (type == null)
            {
                return String.Empty;
            }

            if (field == null)
            {
                return String.Empty;
            }

            var attribute =
                field.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute;
            if (attribute == null)
            {
                return field.Name;
            }

            return attribute.Description;
        }


        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="type">
        /// 类型
        /// </param>
        /// <param name="property">
        /// 成员
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetMemberDescription(Type type, PropertyInfo property)
        {
            if (type == null)
            {
                return String.Empty;
            }

            if (property == null)
            {
                return String.Empty;
            }

            var attribute =
                property.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute;
            if (attribute == null)
            {
                return property.Name;
            }

            return attribute.Description;
        }
        #endregion

        #endregion

        #region 获取实例信息

        /// <summary>
        /// 获取实例的所有公开属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<Tuple<string, string>> GetMembers<T>()
        {
            var result = new List<Tuple<string, string>>();
            var type = typeof(T);
            var members = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            members.IfNotNull(
                              r =>
                              {
                                  r.ForEach(
                                            i =>
                                            {
                                                result.Add(new Tuple<string, string>(i.Name, i.PropertyType.Name));
                                            });
                              });
            return result;
        }

        #endregion
    }
}