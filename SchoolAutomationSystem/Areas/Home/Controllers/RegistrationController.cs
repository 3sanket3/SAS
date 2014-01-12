using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolAutomationSystem.Areas.Home.Models;
namespace SchoolAutomationSystem.Areas.Home.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Home/Registration/
        DataOperations repository = new DataOperations();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClassRegistration()
        {
            ClassDetailViewModel Model = new ClassDetailViewModel();
           // Model.classes = new ClassDetail();
            if (TempData["ClassDetails1"] != null)
            {
                Model = (ClassDetailViewModel)TempData["ClassDetails1"];
                TempData["ClassDetails2"] = TempData["ClassDetails1"];
            }
            else if (TempData["ClassDetails2"] != null)
            {
                Model = (ClassDetailViewModel)TempData["ClassDetails2"];
                TempData["ClassDetails1"] = TempData["ClassDetails2"];
            }
            else
            {
                Model.lstClassDetails = repository.getAllClasses();
                TempData["ClassDetails1"] = Model;
            }
            return View(Model);
        }

        [HttpPost]
        public ActionResult ClassRegistration(ClassDetailViewModel model)
        {
            repository.saveNewClass(model);
            model.lstClassDetails = repository.getAllClasses();
            TempData["ClassDetails1"] = model;
            return View(model);
        }

        [HttpPost]
        public ActionResult GetClassName(int ClassId)
        {
            String classname =repository.getClassNameById(ClassId);
            return Json(classname);
            //return new JsonResult(){ Data = classname };
            //return Json(new { success = true });
            
        }
    }
}
