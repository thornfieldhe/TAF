namespace TAF
{
    using System.Collections.Generic;

    using TAF.Core;
    using TAF.Utility;

    /// <summary>
    /// 业务类监视对象
    /// </summary>
    /// <typeparam name="T">
    /// 业务类
    /// </typeparam>
    public class BaseBusinessEntry<T>
        where T : IEntityBase
    {
        private static readonly Dictionary<string, object> Properties;

        public BaseBusinessEntry(T entity)
        {
            var type = entity.GetType();
            foreach (var pi in type.GetProperties())
            {
                Properties.Add(pi.Name, pi.GetValue(entity, null));
            }

            this.CurrentValues = new PropertyValues(Properties);
            this.OriginalValues = new PropertyValues(Properties);
        }

        /// <summary>
        /// 当前属性值列表
        /// </summary>
        public PropertyValues CurrentValues
        {
            get; private set;
        }

        /// <summary>
        /// 历史属性值列表
        /// </summary>
        public PropertyValues OriginalValues
        {
            get; private set;
        }

        public void UpdateState(string propertyName, object value)
        {
            if (Properties[propertyName] != value)
            {
                Properties[propertyName] = value;
            }

            this.OriginalValues = new PropertyValues(Properties);
        }
    }

    /// <summary>
    /// 业务类属性对象
    /// </summary>
    public class PropertyValues
    {
        private static Dictionary<string, object> Properties = new Dictionary<string, object>();

        public PropertyValues(Dictionary<string, object> properties)
        {
            Properties = properties;
        }

        /// <summary>
        /// 更新属性列表
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void Update(string propertyName, object value)
        {
            Properties[propertyName] = value;
        }

        public T GetValue<T>(string propertyName)
        {
            return this.GetValue(propertyName).CastTo<T>();
        }

        /// <summary>
        /// 如果没有指定属性，则返回一个随机字符串
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public object GetValue(string propertyName)
        {
            return !Properties.ContainsKey(propertyName) ? Randoms.GenerateCheckCode(4) : Properties[propertyName];
        }

        public void SetValue<T>(string propertyName, T value)
        {
            Properties[propertyName] = value;
        }

        public IEnumerable<string> PropertyNames
        {
            get
            {
                return Properties.Keys;
            }
        }

        public object this[string name]
        {
            get
            {
                return GetValue(name);
            }
        }
    }
}