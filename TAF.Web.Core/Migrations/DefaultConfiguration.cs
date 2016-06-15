namespace TAF.Mvc.Business
{
    using System.Data.Entity.Migrations;

    internal sealed class DefaultConfiguration : DbMigrationsConfiguration<TAFContext>
    {
        public DefaultConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
