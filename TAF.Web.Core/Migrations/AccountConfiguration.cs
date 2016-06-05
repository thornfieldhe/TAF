namespace TAF.Mvc.Business
{
    using System.Data.Entity.Migrations;

    internal sealed class AccountConfiguration : DbMigrationsConfiguration<AccountContext>
    {
        public AccountConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
