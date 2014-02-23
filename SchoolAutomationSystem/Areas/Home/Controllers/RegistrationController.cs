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

        public ActionResult DivRegistration()
        {
            DivDetailViewModel Model = new DivDetailViewModel();

            //if (TempData["DivDetails1"] != null)
            //{
            //    Model = (DivDetailViewModel)TempData["DivDetails1"];
            //    TempData["DivDetails2"] = TempData["DivDetails1"];
            //}
            //else if (TempData["DivDetails2"] != null)
            //{
            //    Model = (DivDetailViewModel)TempData["DivDetails2"];
            //    TempData["DivDetails1"] = TempData["DivDetails2"];
            //}
            //else
            //{
              //  Model.lstDivDetails = repository.getAllDiv();
                //TempData["DivDetails1"] = Model;
            //}
            return View(Model);
        }

        [HttpPost]
        public ActionResult DivRegistration(DivDetailViewModel model, FormCollection collaction)
        {
            if (collaction["Action"] == "Submit")
            {
                repository.SaveDivDetails(model);
            }

            model.lstDivDetails = repository.getAllDiv(model.classID);
            return View(model);
        }

        public ActionResult ClassRegistration()
        {
            ClassDetailViewModel Model = new ClassDetailViewModel();
           // Model.classes = new ClassDetail();
            //if (TempData["ClassDetails1"] != null)
            //{
            //    Model = (ClassDetailViewModel)TempData["ClassDetails1"];
            //    TempData["ClassDetails2"] = TempData["ClassDetails1"];
            //}
            //else if (TempData["ClassDetails2"] != null)
            //{
            //    Model = (ClassDetailViewModel)TempData["ClassDetails2"];
            //    TempData["ClassDetails1"] = TempData["ClassDetails2"];
            //}
            //else
            //{
                Model.lstClassDetails = repository.getAllClasses();
                TempData["ClassDetails1"] = Model;
           // }
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

        
        [HttpPost]
        public ActionResult GetDivName(int DivId)
        {
            String Divname = repository.getDivNameById(DivId);
            return Json(Divname);
            //return new JsonResult(){ Data = classname };
            //return Json(new { success = true });

        }

        #region Subject Actions
        public ActionResult SubjectRegistration()
        {
            SubjectDetailViewModel Model = new SubjectDetailViewModel();
            // Model.classes = new ClassDetail();
            //if (TempData["ClassDetails1"] != null)
            //{
            //    Model = (ClassDetailViewModel)TempData["ClassDetails1"];
            //    TempData["ClassDetails2"] = TempData["ClassDetails1"];
            //}
            //else if (TempData["ClassDetails2"] != null)
            //{
            //    Model = (ClassDetailViewModel)TempData["ClassDetails2"];
            //    TempData["ClassDetails1"] = TempData["ClassDetails2"];
            //}
            //else
            //{
            Model.lstSubjectDetails = repository.getAllSubjects();
            TempData["ClassDetails1"] = Model;
            // }
            return View(Model);
        }

        [HttpPost]
        public ActionResult SubjectRegistration(SubjectDetailViewModel model)
        {
            repository.saveNewSubject(model);
            model.lstSubjectDetails = repository.getAllSubjects();
            TempData["ClassDetails1"] = model;
            return View(model);
        }

        [HttpPost]
        public ActionResult GetSubjectName(int SubjectID)
        {
            String SubjectName = repository.getSubjectNameById(SubjectID);
            return Json(SubjectName);
            //return new JsonResult(){ Data = classname };
            //return Json(new { success = true });

        }

        #endregion
    }
}
