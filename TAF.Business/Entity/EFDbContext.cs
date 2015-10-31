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
    public class EFDbContext : DbContext
    {
        public EFDbContext(string connectiion)
        {
        }

        public EFDbContext() : this("DefaultConnection")
        {
        }
    }
}