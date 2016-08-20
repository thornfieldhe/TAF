namespace TAF.Owin
{
    using System.Collections.Generic;
    using System.Web.Http;

    /// <summary>
    /// 
    /// </summary>
    public class AccountController : ApiController
    {
        [AllowAnonymous]
        public IEnumerable<Bug> Get()
        {
            return new List<Bug> { new Bug() { Id = 0, Name = "bug1" } };
        }
    }

    public class Bug
    {
        public string Name
        {
            get; set;
        }

        public int Id
        {
            get; set;
        }
    }
}