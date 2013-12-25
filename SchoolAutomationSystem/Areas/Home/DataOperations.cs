using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolAutomationSystem.Areas.Home.Models;

namespace SchoolAutomationSystem.Areas.Home
{
    public class DataOperations
    {
        HomeEntities HomeEntities;

        public DataOperations()
        {
            HomeEntities = new HomeEntities(); 
        }

        public bool validateLogin(LoginDetails logindetails)
        {

            var password = HomeEntities.Logins.Where(a => a.Username == logindetails.username).Select(a => a.Password).SingleOrDefault();
            //var password = (from login in HomeEntities.Logins
            //                where logindetails.username == login.Username
            //                select login.Password).FirstOrDefault();

            if (password == logindetails.password)
            {
                return true;
            }
            return false;
        }
    }
}