// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Container.cs" company="">
//   
// </copyright>
// <summary>
//   Autofac对象容器
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.DI
{
    using System;
    using System.Reflection;

    using Autofac;
    using Autofac.Core;

    /// <summary>
    /// Autofac对象容器
    /// </summary>
    public class Container
    {
        /// <summary>
        /// 容器
        /// </summary>
        private static IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class. 
        /// 初始化Autofac对象容器
        /// </summary>
        /// <param name="modules">
        /// 配置模块
        /// </param>
        public Container(params IModule[] modules)
            : this(null, modules)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class. 
        /// 初始化Autofac对象容器
        /// </summary>
        /// <param name="action">
        /// 在注册模块前执行的操作
        /// </param>
        /// <param name="modules">
        /// 配置模块
        /// </param>
        public Container(Action<ContainerBuilder> action, params IModule[] modules)
        {
            var builder = CreateBuilder(action, modules);
            _container = builder.Build();
        }

        /// <summary>
        /// 创建容器生成器
        /// </summary>
        /// <param name="modules">
        /// 配置模块
        /// </param>
        /// <returns>
        /// The <see cref="ContainerBuilder"/>.
        /// </returns>
        public static ContainerBuilder CreateBuilder(params IModule[] modules)
        {
            return CreateBuilder(null, modules);
        }

        /// <summary>
        /// 创建容器生成器
        /// </summary>
        /// <param name="action">
        /// 在注册模块前执行的操作
        /// </param>
        /// <param name="modules">
        /// 配置模块
        /// </param>
        /// <returns>
        /// The <see cref="ContainerBuilder"/>.
        /// </returns>
        public static ContainerBuilder CreateBuilder(Action<ContainerBuilder> action, params IModule[] modules)
        {
            var builder = new ContainerBuilder();
            if (action != null)
            {
                action(builder);
            }

            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }

            return builder;
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T">
        /// 对象类型
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T Create<T>() where T : class
        {
            return _container?.Resolve<T>();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">
        /// 对象类型
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object Create(Type type)
        {
            return _container.Resolve(type);
        }

        /// <summary>
        /// 创建对象,适用于基于同一个接口需要创建不同的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T Create<T>(string type)
        {
            return _container.Resolve<T>(new NamedParameter("type", type));
        }

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="action">
        /// 在注册模块前执行的操作
        /// </param>
        /// <param name="modules">
        /// 依赖配置
        /// </param>
        public static void Init(Action<ContainerBuilder> action, params IModule[] modules)
        {
            var builder = CreateBuilder(action, modules);
            _container = builder.Build();
        }

        /// <summary>
        /// 为Mvc注册依赖
        /// </summary>
        /// <param name="mvcAssembly">
        /// mvc项目所在的程序集
        /// </param>
        /// <param name="action">
        /// 在注册模块前执行的操作
        /// </param>
        /// <param name="modules">
        /// 依赖配置
        /// </param>
        public static void RegisterMvc(Assembly mvcAssembly, Action<ContainerBuilder> action, params IModule[] modules)
        {
            var builder = CreateBuilder(action, modules);

            // builder.Register(mvcAssembly);
            _container = builder.Build();

            // DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }
    }
}