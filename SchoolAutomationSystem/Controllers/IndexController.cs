using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolAutomationSystem.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        {
            return RedirectToRoute(new { controller = "Home", action = "Index", area = "Home" });
        }

    }
}
