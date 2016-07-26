// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Role.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   Role
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.Model
{
    using System;

    using TAF.Utility;

    /// <summary>
    /// 角色
    /// </summary>
    public class Role : BaseBusiness<Role>
    {
        public Role()
        {
        }

        public Role(Guid id) : base(id) { }

        #region 属性
        private string name;

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                SetProperty(ref this.name, value);
            }
        }

        #endregion

        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription(nameof(Name), Name.ToStr());
        }
        #endregion
    }
}