// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityTypeConfigurationMap.cs" company="">
//   
// </copyright>
// <summary>
//   EF数据库上下文对象配置基类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Data
{
    using System.Data.Entity.ModelConfiguration;

    using TAF.Core;


    /// <summary>
    /// EF数据库上下文对象配置基类
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class BaseEntityTypeConfigurationMap<T> : EntityTypeConfiguration<T>
        where T : class, IBusinessBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntityTypeConfigurationMap"/> class.
        /// </summary>
        public BaseEntityTypeConfigurationMap()
        {
            this.Property(i => i.Version).IsRowVersion();
            this.Property(i => i.CreatedDate).IsRequired();
            this.Property(i => i.ChangedDate).IsRequired();
        }
    }
}