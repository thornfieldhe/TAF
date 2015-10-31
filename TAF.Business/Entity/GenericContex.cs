// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericContex.cs" company="">
//   
// </copyright>
// <summary>
//   The generic contex.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    /// <summary>
    /// The generic contex.
    /// </summary>
    public class GenericContex
    {
        /// <summary>
        /// The contex key.
        /// </summary>
        private const string ContexKey = "TAF.Context.Web";

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericContex"/> class.
        /// </summary>
        public GenericContex()
        {
            if (isWeb && (HttpContext.Current.Items[ContexKey] == null))
            {
                HttpContext.Current.Items[ContexKey] = new NameBasedDictionary();
            }
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                {
                    return null;
                }

                var cache = GetCache();
                if (cache.Count <= 0)
                {
                    return null;
                }

                object result;
                if (cache.TryGetValue(name, out result))
                {
                    return result;
                }

                return null;
            }

            set
            {
                if (string.IsNullOrEmpty(name))
                {
                    return;
                }

                var cache = GetCache();
                object temp;
                if (cache.TryGetValue(name, out temp))
                {
                    cache[name] = value;
                }
                else
                {
                    cache.Add(name, value);
                }
            }
        }

        /// <summary>
        /// The get cache.
        /// </summary>
        /// <returns>
        /// The <see cref="NameBasedDictionary"/>.
        /// </returns>
        private static NameBasedDictionary GetCache()
        {
            NameBasedDictionary cache;
            if (isWeb)
            {
                cache = (NameBasedDictionary)HttpContext.Current.Items[ContexKey];
            }
            else
            {
                cache = threadCache;
            }

            if (cache == null)
            {
                cache = new NameBasedDictionary();
            }

            if (isWeb)
            {
                HttpContext.Current.Items[ContexKey] = cache;
            }
            else
            {
                threadCache = cache;
            }

            return cache;
        }

        /// <summary>
        /// The check whether is web.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CheckWhetherIsWeb()
        {
            var result = false;
            var domain = AppDomain.CurrentDomain;
            try
            {
                if (domain.ShadowCopyFiles)
                {
                    result = HttpContext.Current.GetType() != null;
                }
            }
            catch (SystemException)
            {
            }

            return result;
        }

        /// <summary>
        /// The name based dictionary.
        /// </summary>
        private class NameBasedDictionary : Dictionary<string, object>
        {
        }

        /// <summary>
        /// The thread cache.
        /// </summary>
        [ThreadStatic]
        private static NameBasedDictionary threadCache;

        /// <summary>
        /// The is web.
        /// </summary>
        private static readonly bool isWeb = CheckWhetherIsWeb();
    }
}