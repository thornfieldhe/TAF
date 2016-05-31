namespace TAF.Mvc.Model
{
    using System;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// 
    /// </summary>
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string name, Guid bussinessId)
            : base(name)
        {
            this.BusinessId = bussinessId;
        }

        public Guid BusinessId
        {
            get; set;
        }
    }
}