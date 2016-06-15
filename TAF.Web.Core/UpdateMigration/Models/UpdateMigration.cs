namespace TAF.Mvc.Model
{
    using TAF.Utility;

    /// <summary>
    /// 数据库更新操作
    /// </summary>
    public class UpdateMigration : BaseBusiness<UpdateMigration>
    {
        public UpdateMigration(IDbProvider dbProvider)
            : base(dbProvider)
        {
        }

        public UpdateMigration()
        {
        }

        /// <summary>
        /// 关键字，用于判断数据库是否已经执行该更新操作
        /// </summary>
        public string Key
        {
            get; set;
        }

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription(nameof(Key), Key.ToStr());
        }
    }
}