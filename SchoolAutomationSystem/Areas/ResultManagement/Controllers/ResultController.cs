using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolAutomationSystem.Areas.ResultManagement.Models;
using SchoolAutomationSystem.Models;

namespace SchoolAutomationSystem.Areas.ResultManagement.Controllers
{
    public class ResultController : Controller
    {
        //
        // GET: /ResultManagement/Result/
        DataOperations repository = new DataOperations();
        public ActionResult BulkResult()
        {
            BulkResultViewModel model = new BulkResultViewModel();
            
            
            return View(model);
        }

        [HttpPost]
        public ActionResult BulkResult(BulkResultViewModel model)
        {
            model.bulkResultData = new List<ListBulkResultData>();
            List<StudentDetail> lstStudents = new List<StudentDetail>();
            lstStudents = repository.GetStudentForClassDiv(model.ClassID, model.DivID);
            foreach (StudentDetail student in lstStudents)
            {
                ListBulkResultData resultData = new ListBulkResultData();
                resultData.StudentID = (int)student.Id;
                resultData.Name = student.FirstName + " " + student.LastName;
                resultData.RollNo = (int)student.RollNo;
                resultData.Marks = (int)repository.GetStudentMarks(model, student.Id);
                model.bulkResultData.Add(resultData);
            }
            return View(model);
        }
        [HttpPost]
        public void SaveBulkMarks(string MarksString, string ClassID, string DivID, string ExamID, string SubjectID, string ActivityID)
        {
            repository.InsertMarks( MarksString, ClassID, DivID, ExamID, SubjectID, ActivityID);
        }
    }
}
