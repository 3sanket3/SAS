using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAutomationSystem.Areas.Home.Models
{
    public class LoginDetails
    {
        public string username { get; set; }
        public string password { get; set; }
        public string errorMessage { get; set; }
        public bool rememberMe { get; set; }
    }
}