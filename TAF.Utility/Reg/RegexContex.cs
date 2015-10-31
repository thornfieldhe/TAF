// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegexContex.cs" company="">
//   
// </copyright>
// <summary>
//   正则上下文
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    using System.Collections.Generic;

    /// <summary>
    /// 正则上下文
    /// </summary>
    public class RegexContex
    {
        /// <summary>
        /// The replacement.
        /// </summary>
        public string Replacement;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexContex"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        public RegexContex(string content)
            : this(content, RegexOperator.Matches)
        {
            this.Content = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexContex"/> class. 
        /// </summary>
        /// <param name="content">
        /// 待匹配字符串
        /// </param>
        /// <param name="operater">
        /// 正则操作
        /// </param>
        public RegexContex(string content, RegexOperator operater)
        {
            this.Content = content;
            this.Operator = operater;
            this.Matches = new List<string>();
            this.Groups = new Dictionary<int, List<string>>();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="RegexContex"/> class from being created.
        /// </summary>
        private RegexContex()
        {
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content
        {
            get; set;
        }

        /// <summary>
        /// Gets the operator.
        /// </summary>
        public RegexOperator Operator
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the matches.
        /// </summary>
        public List<string> Matches
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        public Dictionary<int, List<string>> Groups
        {
            get; set;
        }
    }

    /// <summary>
    /// The regex group.
    /// </summary>
    public class RegexGroup
    {
    }
}