// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Maps.cs" company="">
//   
// </copyright>
// <summary>
//   The user map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.BusinessEntity.Test
{
    using TAF.Data;
    using TAF.Entity;

    /// <summary>
    /// The user map.
    /// </summary>
    internal class UserMap : BaseEntityTypeConfigurationMap<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMap"/> class.
        /// </summary>
        public UserMap()
        {
            this.Property(i => i.Name).IsRequired().HasMaxLength(20);
        }
    }
}