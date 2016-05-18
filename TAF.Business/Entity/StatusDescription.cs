using System.Text;

namespace TAF.Entity
{
    using System;
    using Utility;


    /// <summary>
    /// 要输出实体的状态
    /// </summary>
    [Serializable]
    public abstract class StatusDescription
    {
        /// <summary>
        /// 描述
        /// </summary>
        private StringBuilder description;

        /// <summary>
        /// 输出对象状态
        /// </summary>
        /// <returns>
        /// </returns>
        public override string ToString()
        {
            description = new StringBuilder();
            AddDescriptions();
            return description.ToString().TrimEnd().TrimEnd(',');
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        protected abstract void AddDescriptions();

        /// <summary>
        /// 添加描述
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="value">
        /// </param>
        protected void AddDescription(string title, string value)
        {
            if (title.IsEmpty() || value.IsEmpty())
            {
                return;
            }

            description.Append(string.Format("{title}:'{value}',"));
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        protected void AddDescription<T>(string name, T value)
        {
            if (value.ToStr().IsEmpty())
            {
                return;
            }

            description.AppendFormat("{0}:'{1}',", name, value.ToStr());
        }
    }
}
