// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbContext.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ApplicationUser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF
{
    using System.Data.Entity;

    /// <summary>
    /// 基础ef上下文
    /// </summary>
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(string connectiion)
        {
        }

        public BaseDbContext() : this("DefaultConnection")
        {
        }
    }
}