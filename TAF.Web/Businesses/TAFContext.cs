namespace TAF.Web.Businesses
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class TAFDbContext : IdentityDbContext<ApplicationUser>
    {
        public TAFDbContext()
            : base("DefaultConnection")
        {
        }



        public static TAFDbContext Create()
        {
            return new TAFDbContext();
        }
    }
}