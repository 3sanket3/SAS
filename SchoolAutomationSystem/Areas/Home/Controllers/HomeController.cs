using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolAutomationSystem.Areas.Home.Models;
using SchoolAutomationSystem.Models;
using System.Web.Routing;

namespace SchoolAutomationSystem.Areas.Home.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/Home/
        DataOperations dataOperations = new DataOperations();
        ErrorOperations errorOperations = new ErrorOperations();
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
                    

            try
            {
                if (dataOperations.validateLogin(model))
                {
                    return View("Index");
                }
                model.errorMessage = Constants.Login_Error;
                model.password = string.Empty;
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorDetails errordetails =errorOperations.LogException(Constants.Home_Application, ex);
                           
                TempData["errordetails"] = errordetails;

                RouteValueDictionary rvd = new RouteValueDictionary(new { controller = "Error", action = "ErrorScreen"});

                return RedirectToRoute("Default", rvd);

            }
            
        }

    }
}
