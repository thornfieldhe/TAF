namespace TAF.Mvc.Business
{
    using System;
    using TAF.Mvc.Model;

    /// <summary>
    /// 
    /// </summary>
    public class AccountInitializer : CommonContextSeeder
    {
        private readonly Guid SaUserId = new Guid("6F8DF7E4-2F52-4BBE-A127-7A2DDACA63DB");

        private const string SaUserName = "admin";

        private const string SaUserFullName = "系统管理员";

        private const string SaPassword = "11111111";

        private const string Role_Admins = "系统管理员组";

        private readonly Guid Role_AdminId = new Guid("7CD116ED-B1B8-4AF9-887F-3CEADF761750");

        private const string Role_User = "用户组";

        private readonly Guid Role_UserId = new Guid("88DB4CEF-6207-4641-94DA-AFF393FBCDB2");



        public AccountInitializer(IDbProvider provider, string key) : base(provider, key) { }

        public override void UpdateData()
        {
            var user = new User(SaUserId) { DbProvider = this.Provider, UserName = SaUserName, FullName = SaUserFullName };
            user.Register(SaPassword, Guid.Empty, false);

            var roleAdmin = new Role(this.Role_AdminId) { DbProvider = this.Provider, Name = Role_Admins };
            var roleUser = new Role(this.Role_UserId) { DbProvider = this.Provider, Name = Role_User };
            roleAdmin.Create(Guid.Empty, false);
            roleUser.Create(Guid.Empty, false);

            var userRoleAdmin = new UserRoles() { DbProvider = this.Provider, UserId = user.Id, RoleId = roleAdmin.Id };
            var userRoleUser = new UserRoles() { DbProvider = this.Provider, UserId = user.Id, RoleId = roleUser.Id };
            userRoleAdmin.Create(Guid.Empty, false);
            userRoleUser.Create(Guid.Empty, false);
        }
    }
}