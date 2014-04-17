using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAutomationSystem.Areas.Home.Models
{
    public class StudentDetailsViewModel
    {

        public SearchStudentData SearchSrudetnData { get; set; }
        public List<StudentDetail> LstStudentDetails { get; set; }
        public StudentDetail StudentProcessingData { get; set; }
    }
    public class SearchStudentData
    {
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public int GRNO { get; set; }
        public int RollNo { get; set; }
        public int StudentId { get; set; }
        public string ClassName { get; set; }
        public string DivName { get; set; }
    }

}