namespace TAF
{
    using Castle.Core.Interceptor;
    using Castle.DynamicProxy;

    using TAF.Utility;

    /// <summary>
    /// 动态代理实例
    /// </summary>
    public class Proxy : SingletonBase<Proxy>
    {
        private readonly ProxyGenerator _proxyGenerator;
        public Proxy()
        {
            _proxyGenerator = new ProxyGenerator();
        }

        /// <summary>
        /// 创建类代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="interceptors"></param>
        /// <returns></returns>
        public T CreateClassProxy<T>(params IInterceptor[] interceptors) where T : class
        {
            return _proxyGenerator.CreateClassProxy<T>(interceptors);
        }

        /// <summary>
        /// 创建接口代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="interceptors"></param>
        /// <returns></returns>
        public T CreateInterfaceProxy<T>(T target, params IInterceptor[] interceptors) where T : class
        {
            return _proxyGenerator.CreateInterfaceProxyWithTarget<T>(target, interceptors);
        }
    }
}