namespace TAF.Web.Businesses
{


    using TAF.MVC.Businesses;

    public class WebDbContext : TAFDbContext
    {
        public new static WebDbContext Create()
        {
            return new WebDbContext();
        }
    }
}