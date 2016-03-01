namespace TAF.Web.Businesses
{
    using System.Data.Entity;

    /// <summary>
    /// 
    /// </summary>
    public class DbInitializer : CreateDatabaseIfNotExists<WebDbContext>
    {
        //        protected override void Seed(WebDbContext context)
        //        {
        //            base.Seed(context);
        //        }
    }
}