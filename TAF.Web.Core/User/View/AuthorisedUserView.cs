// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorisedUserView.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   AuthorisedUserView
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.View
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 已授权用户视图
    /// </summary>
    public class AuthorisedUserView
    {
        public Guid Id
        {
            get;
            set;
        }


        public List<string> Roles
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string FullName
        {
            get; set;
        }
    }
}