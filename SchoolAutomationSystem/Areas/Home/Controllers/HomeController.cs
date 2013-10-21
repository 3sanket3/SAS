using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolAutomationSystem.Areas.Home.Models;

namespace SchoolAutomationSystem.Areas.Home.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/Home/
        DataOperations dataOperations = new DataOperations();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            LoginDetails loginDetails = new LoginDetails();
            
            return View(loginDetails);
        }

        [HttpPost]
        public ActionResult Login(LoginDetails model)
        {

            if (dataOperations.validateLogin(model))
            {
                return View("Index");
            }
            model.errorMessage = Constants.Login_Error;
            model.password = string.Empty;
            return View(model);
        }

    }
}
