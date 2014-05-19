using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolAutomationSystem.Areas.ResultManagement.Models
{
    public class BulkResultViewModel
    {
        DataOperations repository = new DataOperations();
        public int ResultID { get; set; }
        public int ClassID { get; set; }
        public int DivID { get; set; }
        public int SubjectID { get; set; }
        public int ExamID { get; set; }
        public int ActivityID { get; set; }
        public int StudentID { get; set; }
        public int Marks { get; set; }
        public List<ListBulkResultData> bulkResultData { get; set; }
        public SelectList GetAllDivSelectList(int selectedValues)
        {
            return repository.GetAllDivSelectList(selectedValues);
        }
        public SelectList getExamList(int selectedValues)
        {
            return repository.getExamList(selectedValues);
        }
        public SelectList GetAllClassesSelectList(int selectedValues)
        {
            return repository.GetAllClassesSelectList(selectedValues);
        }
        public SelectList GetSubjectsSelectList(int selectedValues)
        {
            return repository.GetSubjectsSelectList(selectedValues);
        }


        public SelectList GetActivitySelectList(int selectedValues)
        {
            return repository.GetActivitySelectList(selectedValues);
        }
    }

    public class ListBulkResultData
    {
        public int ResultID { get; set; }
        public int RollNo { get; set; }
        public int StudentID { get; set; }
        public string Name { get; set; }
        public int? Marks { get; set; }
    }
}