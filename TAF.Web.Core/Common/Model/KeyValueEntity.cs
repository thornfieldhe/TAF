// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyValueEntity.cs" company="">
//   
// </copyright>
// <summary>
//   简单键值对象
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc
{
    public class KeyValueEntity<T, K>
    {
        public T Id
        {
            get; set;
        }

        public K Name
        {
            get; set;
        }
    }
}