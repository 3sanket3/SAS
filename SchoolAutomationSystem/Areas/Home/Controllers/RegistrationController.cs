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

        #region Div Actions

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



        [HttpPost]
        public ActionResult GetDivName(int DivId)
        {
            String Divname = repository.getDivNameById(DivId);
            return Json(Divname);
            //return new JsonResult(){ Data = classname };
            //return Json(new { success = true });

        }

#endregion

        #region Class Actions

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

        #endregion

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

        #region Faculty Actions

        public ActionResult FacultySearch()
        {
            FacultyDetailsViewModel model = new FacultyDetailsViewModel();
            model.LstFacultyDetails = repository.getFacultyDetailsList();
            return View(model);
        }

        [HttpPost]
        public ActionResult FacultySearch(FacultyDetailsViewModel model, FormCollection collection)
        {

            if (collection["Action"] != "Search")
            {
                repository.SaveFacultyDetails(model.FacultyProcessingData);
                model.LstFacultyDetails = repository.getFacultyDetailsList();
            }
            else
            {
                FacultyDetail searchFacultyDetails = new FacultyDetail();
                searchFacultyDetails.FacultyUniqueName= collection["Model.UniqueName"];
                searchFacultyDetails.FirstName = collection["Model.Name"];
                searchFacultyDetails.EmailId = collection["Model.EmailAddress"];
                model.LstFacultyDetails= repository.SearchFaculties(searchFacultyDetails);

            }
            
            
            return View(model);
        }

        //public ActionResult FacultyDetailsProcessing(int id)
        //{
        //    FacultyDetail Faculty = new FacultyDetail();

        //    Faculty = repository.getFacultyDetailFromID(id);

        //    return View(Faculty);

        //}
        //[HttpPost]
        //public ActionResult FacultyDetailsProcessing(FacultyDetailsViewModel model)
        //{
        //    FacultyDetail Faculty = new FacultyDetail();

        //    Faculty = repository.getFacultyDetailFromID(model.FacultyId);

        //    return View(Faculty);

        //}

        public ActionResult GetFacultyDetailView(int FacultyId)
        {
            FacultyDetailsViewModel FacultyDetailsViewModel = new FacultyDetailsViewModel();
            if (FacultyId > 0)
            {
                FacultyDetailsViewModel.FacultyProcessingData = repository.getFacultyDetailFromID(FacultyId);
            }
            else
            {
                FacultyDetailsViewModel.FacultyProcessingData = new FacultyDetail();
            }
            return View("FacultyDetailsProcessing", FacultyDetailsViewModel);
        }
        #endregion

        #region Student Actions

        public ActionResult StudentSearch()
        {
            StudentDetailsViewModel model = new StudentDetailsViewModel();
            model.LstStudentDetails = repository.getStudentDetailsList();
            return View(model);
        }

        [HttpPost]
        public ActionResult StudentSearch(StudentDetailsViewModel model, FormCollection collection)
        {

            if (collection["Action"] != "Search")
            {
                repository.SaveFacultyDetails(model.StudentProcessingData);
                model.LstStudentDetails = repository.getStudentDetailsList();
            }
            else
            {
                SearchStudentData searchStudentData = new SearchStudentData();
                model.LstStudentDetails = repository.SearchStudents(searchStudentData);

            }


            return View(model);
        }

        //public ActionResult FacultyDetailsProcessing(int id)
        //{
        //    FacultyDetail Faculty = new FacultyDetail();

        //    Faculty = repository.getFacultyDetailFromID(id);

        //    return View(Faculty);

        //}
        //[HttpPost]
        //public ActionResult FacultyDetailsProcessing(FacultyDetailsViewModel model)
        //{
        //    FacultyDetail Faculty = new FacultyDetail();

        //    Faculty = repository.getFacultyDetailFromID(model.FacultyId);

        //    return View(Faculty);

        //}

        //public ActionResult GetFacultyDetailView(int FacultyId)
        //{
        //    FacultyDetailsViewModel FacultyDetailsViewModel = new FacultyDetailsViewModel();
        //    if (FacultyId > 0)
        //    {
        //        FacultyDetailsViewModel.FacultyProcessingData = repository.getFacultyDetailFromID(FacultyId);
        //    }
        //    else
        //    {
        //        FacultyDetailsViewModel.FacultyProcessingData = new FacultyDetail();
        //    }
        //    return View("FacultyDetailsProcessing", FacultyDetailsViewModel);
        //}



        #endregion


    }
}
