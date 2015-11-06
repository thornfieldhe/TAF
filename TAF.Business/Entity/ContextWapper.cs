namespace TAF.Entity
{
    using System.Data.Entity;
    using TAF.Core;
    using TAF.Utility;

    /// <summary>
    /// 上下文包装类用于封装Contex
    /// </summary>
    internal class ContextWapper : IContextWapper
    {
        private const string connection = "EFConnection";
        public DbContext Context => connection.IsEmpty() ? new BaseDbContext() : new BaseDbContext(connection);
    }
}
