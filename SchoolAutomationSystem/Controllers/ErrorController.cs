using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolAutomationSystem.Models;

namespace SchoolAutomationSystem.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult ErrorScreen(ErrorDetails errorDetails)
        {
            errorDetails = (ErrorDetails)TempData["errordetails"];

            return View(errorDetails);
        }

        //public ActionResult ErrorScreen()
        //{
        //    return View();
        //}
    }
}
