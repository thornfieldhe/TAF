namespace TAF.Business
{
    using System;
    using System.Reflection;

    /// <summary>
    /// 卸载步骤
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TearDownStepAttribute : Attribute, IComparable, IBuildStep
    {
        public TearDownStepAttribute(int sequence, int times = 1)
        {
            this.Sequence = sequence;
            this.Times = times;
        }

        public MethodInfo Handler
        {
            get; set;
        }

        /// <summary>
        /// 执行顺序
        /// </summary>
        public int Sequence
        {
            get;
        }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int Times
        {
            get;
        }

        public int CompareTo(object obj)
        {
            if (obj == null || obj.GetType() != typeof(TearDownStepAttribute))
            {
                throw new ArgumentException("obj");
            }

            return this.Sequence - ((TearDownStepAttribute)obj).Sequence;
        }
    }
}