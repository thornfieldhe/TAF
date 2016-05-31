namespace TAF.Mvc.Model
{
    /// <summary>
    /// 数据库更新操作
    /// </summary>
    public class UpdateMigration : BaseBusiness<UpdateMigration>
    {
        /// <summary>
        /// 关键字，用于判断数据库是否已经执行该更新操作
        /// </summary>
        public string Key
        {
            get; set;
        }
    }
}