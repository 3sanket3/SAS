using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolAutomationSystem.Models;
namespace SchoolAutomationSystem.Areas.Home.Models
{
    public class ActivityDetailsViewModel
    {
        DataOperations repository = new DataOperations();
        public int ClassId { get; set; }
        public int DivId { get; set; }
        public int SubjectID { get; set; }
        public int ExamId { get; set; }
        public string ActivityName { get; set; }
        public DateTime date { get; set; }
        public int TotalMarks { get; set; }
        public ActivityDetail ActivityDetail { get; set; }
        public List<ActivityDetail> lstActivityDetails { get; set; }

        
        public SelectList GetAllClassesSelectList(int selectedValue)
        {
            SelectList allClasses = repository.GetAllClassesSelectList(selectedValue);
            return allClasses;

        }
        public SelectList getAllDiv(int selectedValue)
        {
            return repository.GetAllDivSelectList(selectedValue);
        }

        public SelectList getFExamList(int selectedValue)
        {
            return repository.getFExamList(selectedValue);
        }
        public SelectList GetAllSubjectSelectList(int selectedValues)
        {
            return repository.GetAllSubjectSelectList(selectedValues);
        }
    }
    public class JSonReturnActivityData
    {
        public int ActivityID { get; set; }
        public int ClassID { get; set; }
        public int DivID { get; set; }
        public int SubjectID { get; set; }
        public int ExamID { get; set; }
        public string ActivityName { get; set; }
        public int TotalMarks { get; set; }
        public string date { get; set; }
    }
}
