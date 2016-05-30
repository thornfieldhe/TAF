namespace TAF.Business
{
    using System;
    using System.Collections.Generic;

    using TAF.Utility;

    /// <summary>
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class Builder<T> : IBuilder<T> where T : class, new()
    {
        /// <summary>
        /// 装配
        /// </summary>
        /// <returns></returns>
        public T BuildUp()
        {
            return Build<BuildStepAttribute>();
        }

        /// <summary>
        /// 卸载
        /// </summary>
        /// <returns></returns>
        public virtual T TearDown()
        {
            return Build<TearDownStepAttribute>();
        }


        private static T Build<K>() where K : Attribute, IBuildStep
        {
            var attributes = DiscoveryBuildSteps<K>();
            if (attributes == null)
            {
                return new T();
            }

            var target = new T();
            foreach (var buildStepAttribute in attributes)
            {
                for (var i = 0; i < buildStepAttribute.Times; i++)
                {
                    buildStepAttribute.Handler.Invoke(target, null);
                }
            }

            return target;
        }

        private static IList<K> DiscoveryBuildSteps<K>() where K : Attribute, IBuildStep
        {
            var methods = Reflection.GetMethodsWithCustomAttribute<K>(typeof(T));
            if (methods == null || methods.Count == 0)
            {
                return null;
            }

            var attributes = new K[methods.Count];
            for (var i = 0; i < methods.Count; i++)
            {
                var attribute = Reflection.GetMethodCustomAttribute<K>(methods[i]);
                attribute.Handler = methods[i];
                attributes[i] = attribute;
            }

            Array.Sort(attributes);
            return new List<K>(attributes);
        }
    }
}