using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolAutomationSystem.Areas.Home.Models;
using SchoolAutomationSystem.Models;
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
            Model.SuccessErrorMsg = "";
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
                if (repository.IsDivExist(model.classID, model.divName))
                {
                    model.SuccessErrorMsg = "<div id='SuccessErrorMessage' style = 'color : Red'> Div Already Exist </div>";
                }
                else
                {
                    repository.SaveDivDetails(model);
                    model.SuccessErrorMsg = "<div id='SuccessErrorMessage' style = 'color : Green'>  Div Successfully Added  </div>";
                }
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
                Model.SuccessErrorMsg = "";
                TempData["ClassDetails1"] = Model;
           // }
            return View(Model);
        }

        [HttpPost]
        public ActionResult ClassRegistration(ClassDetailViewModel model)
        {
            if (repository.IsClassExist(model.className))
            {
                model.SuccessErrorMsg = "<div id='SuccessErrorMessage' style = 'color : Red'> Class Already Exist </div>";
            }
            else
            {
               repository.saveNewClass(model);
               model.SuccessErrorMsg = "<div id='SuccessErrorMessage' style = 'color : Green'>  Class Successfully Added  </div>";
            }
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
            Model.SuccessErrorMsg = "";
            Model.lstSubjectDetails = repository.getAllSubjects();
            TempData["ClassDetails1"] = Model;
            // }
            return View(Model);
        }

        [HttpPost]
        public ActionResult SubjectRegistration(SubjectDetailViewModel model)
        {
            if(repository.IsSubjectExist(model.SubjectName.Trim()))
            {
                model.SuccessErrorMsg = "<div id='SuccessErrorMessage' style = 'color : Red'> Subject Already Exist </div>";    
            }
            else{
                   repository.saveNewSubject(model);
                   model.SuccessErrorMsg = "<div id='SuccessErrorMessage' style = 'color : Green'>  Subject Successfully Added  </div>";
            }
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
                if (repository.IsFacultyExist((int)model.FacultyProcessingData.FacultyId ,model.FacultyProcessingData.FacultyUniqueName.Trim()))
                {
                    model.SuccessErrorMsg = "<div id='SuccessErrorMessage' style = 'color : Red'> Faculty already exist with  Unique name '" + model.FacultyProcessingData.FacultyUniqueName.Trim() + "' </div>"; 
                }
                else
                {
                    repository.SaveFacultyDetails(model.FacultyProcessingData);
                    model.SuccessErrorMsg = "<div id='SuccessErrorMessage' style = 'color : Green'>  Faculty Successfully Added  </div>";
                }
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
            model.SearchSrudetnData = new SearchStudentData();

            return View(model);
        }

        [HttpPost]
        public ActionResult StudentSearch(StudentDetailsViewModel model, FormCollection collection)
        {
            string SuccessErrorMsg = "";
            if (collection["Action"] != "Search")
            {
                if (repository.IsStudentExist(model.StudentProcessingData, out SuccessErrorMsg))
                {
                    model.SuccessErrorMsg = SuccessErrorMsg;
                    model.KeepViewOpen = true;
                }
                else
                {
                    repository.SaveFacultyDetails(model.StudentProcessingData);
                    model.SuccessErrorMsg = "<div id='SuccessErrorMessage' style = 'color : Green'>  Student Successfully Added  </div>";
                }
                model.LstStudentDetails = repository.getStudentDetailsList();
            }
            else
            {
                //SearchStudentData searchStudentData = new SearchStudentData();
                model.LstStudentDetails = repository.SearchStudents(model.SearchSrudetnData);

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

        public ActionResult GetStudentDetailView(int StudentId)
        {
            StudentDetailsViewModel studentDetailsViewModel = new StudentDetailsViewModel();
            if (StudentId > 0)
            
                studentDetailsViewModel.StudentProcessingData = repository.getStudentDetailFromID(StudentId);
            
            else
            {
                studentDetailsViewModel.StudentProcessingData = new StudentDetail();
            }
            return View("StudentDetailsProcessing", studentDetailsViewModel);
        }

        

        #endregion

        #region SubjectMapping Screen
        public ActionResult GetAllFacultiesSelectList()
        {
            return Json( repository.GetAllFacultiesSelectList(0));
        }
        public ActionResult GetAllSubjectsSelectList()
        {
            return Json(repository.GetAllSubjectSelectList(0));
        }
        public ActionResult SubjectMapping()
        {
            SubjectMappingViewModel model = new SubjectMappingViewModel();
            model.SubjectMapping = new SubjectMapping();
            model.lstSubjectDetails = repository.getAllSubjectMapping(model.SubjectMapping);

            return View(model);
        }
        [HttpPost]
        public ActionResult SubjectMapping(SubjectMappingViewModel model,FormCollection collection)
        {
            //if (collection["Action"] == "GetMapping")
            //{
                
            //}
            //else 
           if (collection["Action"] == "AddMapping")
            {
                repository.SaveMapping(model);
            }
            model.lstSubjectDetails = repository.getAllSubjectMapping(model.SubjectMapping);

            return View(model);
        }
      
        public ActionResult GetMapptingDtails(int MappingID)
        {
            JSonReturnData mapping= repository.GetSubjectMappingDetails(MappingID);
            string JsonString = repository.JsonSerializer<JSonReturnData>(mapping);
            return Json(JsonString,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ActivityScreen
       
        public ActionResult ActivityRegistration()
        {
            ActivityDetailsViewModel model = new ActivityDetailsViewModel();
            model.ActivityDetail = new ActivityDetail();
            model.lstActivityDetails = repository.getAllActivity(model.ActivityDetail);

            return View(model);
        }
        [HttpPost]
        public ActionResult ActivityRegistration(ActivityDetailsViewModel model, FormCollection collection)
        {
            //if (collection["Action"] == "GetMapping")
            //{

            //}
            //else 
            if (collection["Action"] == "AddActivity")
            {
                repository.SaveActivity(model);
            }
            model.lstActivityDetails = repository.getAllActivity(model.ActivityDetail);
            model.ActivityDetail.ActivityName = string.Empty;
            model.ActivityDetail.Date = null;
            model.ActivityDetail.TotalMarks = 0;
            return View(model);
        }

        public ActionResult GetActivityDtails(int ActivityID)
        {
            JSonReturnActivityData Activities = repository.GetActivityDetails(ActivityID);
            string JsonString = repository.JsonSerializer<JSonReturnActivityData>(Activities);
            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
