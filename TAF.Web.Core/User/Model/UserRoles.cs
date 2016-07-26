// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRoles.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   UserRoles
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Model
{
    using System;

    using TAF.Utility;

    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRoles : BaseBusiness<UserRoles>
    {
        public UserRoles()
        {
        }

        public UserRoles(Guid id) : base(id)
        {
        }

        #region 属性
        private Guid userId;
        private Guid roleId;

        public Guid UserId
        {
            get
            {
                return this.userId;
            }

            set
            {
                SetProperty(ref this.userId, value);
            }
        }

        public Guid RoleId
        {
            get
            {
                return this.roleId;
            }

            set
            {
                SetProperty(ref this.roleId, value);
            }
        }
        #endregion

        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription(nameof(RoleId), RoleId.ToStr());
            AddDescription(nameof(UserId), UserId.ToStr());
        }
        #endregion
    }
}