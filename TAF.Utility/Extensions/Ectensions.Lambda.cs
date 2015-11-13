// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ectensions.Lambda.cs" company="">
//   
// </copyright>
// <summary>
//   The where if extension.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The where if extension.
    /// </summary>
    public static class WhereIfExtension
    {
        /// <summary>
        /// The where if.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            Expression<Func<T, bool>> predicate,
            bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// The where if.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            Expression<Func<T, int, bool>> predicate,
            bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// The where if.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// IEnumerable<T>
        /// </returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// The where if.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<T> WhereIf<T>(
            this IEnumerable<T> source,
            Func<T, int, bool> predicate,
            bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}