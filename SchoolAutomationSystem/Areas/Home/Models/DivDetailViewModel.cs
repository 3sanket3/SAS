using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolAutomationSystem.Areas.Home.Models
{
    public class DivDetailViewModel
    {
        public List<DivDetail> lstDivDetails = new List<DivDetail>();
        public string divName { get; set; }
        public int divId { get; set; }
        public int classID { get; set; }
        DataOperations repository = new DataOperations();

        public SelectList getAllClassNames()
        {
            SelectList allClasses = repository.getAllClassesInDropDownList();
            return allClasses;
            //return new SelectList() { };
        }
    }
}