namespace TAF.Web.Migrations
{
    using System.Data.Entity.Migrations;

    using TAF.Web.Businesses;

    internal sealed class Configuration : DbMigrationsConfiguration<WebDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
