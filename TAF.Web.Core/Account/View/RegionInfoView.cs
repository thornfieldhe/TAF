// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegionInfoView.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   RegionInfoView
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Mvc.View
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 
    /// </summary>
    public class RegionInfoView
    {
        [Required]
        [EmailAddress]
        public string Email
        {
            get; set;
        }

        [Required]
        public string UserName
        {
            get; set;
        }

        public string FullName
        {
            get; set;
        }

        [Required]
        public string Password
        {
            get; set;
        }

        public string[] Roles
        {
            get; set;
        }
    }
}