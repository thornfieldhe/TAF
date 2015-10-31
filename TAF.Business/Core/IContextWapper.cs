// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IContextWapper.cs" company="">
//   
// </copyright>
// <summary>
//   The ContextWapper interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Data.Entity;

namespace TAF.Core
{
    /// <summary>
    /// The ContextWapper interface.
    /// </summary>
    public interface IContextWapper
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        DbContext Context
        {
            get;
        }
    }
}