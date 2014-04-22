using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolAutomationSystem.Areas.Home.Models
{
    public class StudentDetailsViewModel
    {
        DataOperations repository = new DataOperations();
        public int StudentId { get; set; }
        public SearchStudentData SearchSrudetnData { get; set; }
        public List<StudentDetail> LstStudentDetails { get; set; }
        public StudentDetail StudentProcessingData { get; set; }

        public SelectList GetAllClassesSelectList(int selectedValue)
        {
            SelectList allClasses = repository.GetAllClassesSelectList(selectedValue);
            return allClasses;
            
        }
        public SelectList getAllDiv(int selectedValue)
        {
            return repository.GetAllDivSelectList(selectedValue);
        }
    }
    public class SearchStudentData
    {
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public int GRNO { get; set; }
        public int RollNo { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public int DivId { get; set; }
    }
    
}