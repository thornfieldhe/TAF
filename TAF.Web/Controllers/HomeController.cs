// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="">
//   
// </copyright>
// <summary>
//   The HomeController .
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Web.Controllers
{
    using System.Web.Mvc;
    using TAF.MVC;

    /// <summary>
    /// The HomeController controller.
    /// </summary>
    [Authorize]
    public class HomeController : BaseHomeController
    {

    }
}