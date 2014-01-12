using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace SchoolAutomationSystem.Areas.Home.Models
{
    public class ClassDetailViewModel
    {
        public IEnumerable<ClassDetail> lstClassDetails = new List<ClassDetail>();
//        public ClassDetail classes = new ClassDetail();
        public string className { get; set; }
        public int id { get; set; }

    }
}