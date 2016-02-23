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
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    using Newtonsoft.Json;

    using TAF.Mvc;
    using TAF.Utility;
    using TAF.Web.Models;

    /// <summary>
    /// The HomeController controller.
    /// </summary>
    [Authorize]
    public class HomeController : BaseHomeController
    {
        
    }
}