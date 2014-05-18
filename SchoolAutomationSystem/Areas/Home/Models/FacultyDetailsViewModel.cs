using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolAutomationSystem.Models;

namespace SchoolAutomationSystem.Areas.Home.Models
{
    public class FacultyDetailsViewModel
    {
        public int FacultyId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string UniqueName { get; set; }
        public List<FacultyDetail> LstFacultyDetails = new List<FacultyDetail>();
        public FacultyDetail FacultyProcessingData { get; set; }

    }
}