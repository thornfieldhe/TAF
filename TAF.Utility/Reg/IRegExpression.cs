// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegExpression.cs" company="">
//   
// </copyright>
// <summary>
//   正则表达式接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    /// <summary>
    /// 正则表达式接口
    /// </summary>
    public interface IRegExpression : IExpression
    {
        /// <summary>
        /// 是否匹配
        /// </summary>
        /// <param name="expression">
        /// 待匹配表达式
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsMatch(string expression);
    }
}