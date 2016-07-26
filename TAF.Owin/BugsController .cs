namespace TAF.Owin
{
    using System.Collections.Generic;
    using System.Web.Http;

    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class AccountController : ApiController
    {
        public IEnumerable<Bug> LoginIn()
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