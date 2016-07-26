// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitData.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   InitData
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Owin
{

    using TAF.Mvc.Business;
    using TAF.MVC.Common.Businesses;

    /// <summary>
    /// 
    /// </summary>
    public class InitData : BaseInitData
    {
        protected override void CustumerContextMigrate(IDbProvider provider)
        {
            base.CustumerContextMigrate(provider);
        }
    }
}