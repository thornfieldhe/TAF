// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyValue.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   键值对对象
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace TAF.Utility
{
    /// <summary>
    ///     名值对对象
    /// </summary>
    /// <typeparam name="K1">
    /// </typeparam>
    /// <typeparam name="K2">
    /// </typeparam>
    [DataContract]
    [Serializable]
    public class KeyValue<K1, K2>
    {
        /// <summary>
        /// </summary>
        public KeyValue()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        public KeyValue(K1 item1, K2 item2)
        {
            this.Key   = item1;
            this.Value = item2;
        }

        /// <summary>
        /// </summary>
        public K1 Key { get; set; }

        /// <summary>
        /// </summary>
        public K2 Value { get; set; }
    }

    /// <summary>
    ///     名值对对象K1,K2,K3
    /// </summary>
    /// <typeparam name="K1"></typeparam>
    /// <typeparam name="K2"></typeparam>
    /// <typeparam name="K3"></typeparam>
    [DataContract]
    [Serializable]
    public class KeyValue<K1, K2, K3> : KeyValue<K1, K2>
    {
        public KeyValue()
        {
        }

        public KeyValue(K1 item1, K2 item2, K3 item3)
            : base(item1, item2)
        {
            this.Item3 = item3;
        }

        public K3 Item3 { get; set; }
    }

    /// <summary>
    ///     名值对对象K1,K2,K3,K4
    /// </summary>
    /// <typeparam name="K1">
    /// </typeparam>
    /// <typeparam name="K2">
    /// </typeparam>
    /// <typeparam name="K3">
    /// </typeparam>
    /// <typeparam name="K4">
    /// </typeparam>
    [DataContract]
    [Serializable]
    public class KeyValue<K1, K2, K3, K4> : KeyValue<K1, K2, K3>
    {
        /// <summary>
        /// </summary>
        public KeyValue()
        {
        }

        public KeyValue(K1 item1, K2 item2, K3 item3, K4 item4)
            : base(item1, item2, item3)
        {
            this.Item4 = item4;
        }

        public K4 Item4 { get; set; }
    }

    /// <summary>
    ///     名值对对象K1,K2,K3,K4,K5
    /// </summary>
    /// <typeparam name="K1">
    /// </typeparam>
    /// <typeparam name="K2">
    /// </typeparam>
    /// <typeparam name="K3">
    /// </typeparam>
    /// <typeparam name="K4">
    /// </typeparam>
    /// <typeparam name="K5"></typeparam>
    [DataContract]
    [Serializable]
    public class KeyValue<K1, K2, K3, K4, K5> : KeyValue<K1, K2, K3, K4>
    {
        /// <summary>
        /// </summary>
        public KeyValue()
        {
        }

        public KeyValue(K1 item1, K2 item2, K3 item3, K4 item4, K5 item5)
            : base(item1, item2, item3, item4)
        {
            this.Item5 = item5;
        }

        public K5 Item5 { get; set; }
    }

    /// <summary>
    ///     名值对对象K1,K2,K3,K4,K5,K6
    /// </summary>
    /// <typeparam name="K1">
    /// </typeparam>
    /// <typeparam name="K2">
    /// </typeparam>
    /// <typeparam name="K3">
    /// </typeparam>
    /// <typeparam name="K4">
    /// </typeparam>
    /// <typeparam name="K5"></typeparam>
    /// <typeparam name="K6"></typeparam>
    [DataContract]
    [Serializable]
    public class KeyValue<K1, K2, K3, K4, K5, K6> : KeyValue<K1, K2, K3, K4, K5>
    {
        /// <summary>
        /// </summary>
        public KeyValue()
        {
        }

        public KeyValue(K1 item1, K2 item2, K3 item3, K4 item4, K5 item5, K6 item6)
            : base(item1, item2, item3, item4, item5)
        {
            this.Item6 = item6;
        }

        public K6 Item6 { get; set; }
    }

    /// <summary>
    ///     名值对对象K1,K2,K3，K4,K5,K6，K7
    /// </summary>
    /// <typeparam name="K1">
    /// </typeparam>
    /// <typeparam name="K2">
    /// </typeparam>
    /// <typeparam name="K3">
    /// </typeparam>
    /// <typeparam name="K4">
    /// </typeparam>
    /// <typeparam name="K5">
    /// </typeparam>
    /// <typeparam name="K6">
    /// </typeparam>
    /// <typeparam name="K7">
    /// </typeparam>
    [DataContract]
    [Serializable]
    public class KeyValue<K1, K2, K3, K4, K5, K6, K7> : KeyValue<K1, K2, K3, K4, K5, K6>
    {
        /// <summary>
        /// </summary>
        public KeyValue()
        {
        }

        public KeyValue(K1 item1, K2 item2, K3 item3, K4 item4, K5 item5, K6 item6, K7 item7)
            : base(item1, item2, item3, item4, item5, item6)
        {
            this.Item4 = item4;
        }

        public K7 Item7 { get; set; }
    }
}
