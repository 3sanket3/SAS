using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolAutomationSystem.Models;

namespace SchoolAutomationSystem.Areas.Home.Models
{
    public class SubjectDetailViewModel
    {
        public IEnumerable<SubjectDetail> lstSubjectDetails = new List<SubjectDetail>();
        //        public ClassDetail classes = new ClassDetail();
        public string SubjectName { get; set; }
        public int ID { get; set; }
    }
}