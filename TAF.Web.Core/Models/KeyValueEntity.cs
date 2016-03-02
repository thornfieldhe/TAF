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
    using System;
    using TAF.Core;

    public class KeyValueEntity : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
    }
}