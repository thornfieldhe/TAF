// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountRoleManager.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   AccountRoleManager
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Business
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    using TAF.Mvc.Model;

    /// <summary>
    /// 
    /// </summary>
    public class AccountRoleManager : RoleManager<ApplicationRole>
    {
        public AccountRoleManager(IRoleStore<ApplicationRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static AccountRoleManager CreateForAccount(TAFContext db)
        {
            if (db == null)
                db = TAFContext.Create();
            var manager = new AccountRoleManager(new RoleStore<ApplicationRole>(db));

            return manager;
        }

        public static AccountRoleManager CreateForOwin(IdentityFactoryOptions<AccountRoleManager> options, IOwinContext context)
        {
            var db = context == null ? null : context.Get<TAFContext>();
            return CreateForAccount(db);
        }
    }
}