namespace TAF
{

    using Castle.Core.Interceptor;

    /// <summary>
    /// 属性修改拦截器
    /// </summary>
    public class NotifyPropertyChangedInterceptor : StandardInterceptor
    {
        protected override void PostProceed(IInvocation invocation)
        {
            base.PostProceed(invocation);
            var methodName = invocation.Method.Name;
            if (!methodName.StartsWith("set_"))
            {
                return;
            }
            var propertyName = methodName.Substring(4);
            var target = invocation.Proxy as NotifyPropertyChanged;
            target?.OnPropertyChanged(propertyName);
        }
    }
}