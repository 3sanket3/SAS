using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolAutomationSystem.Areas.Home.Models;

namespace SchoolAutomationSystem.Areas.Home
{
    public class DataOperations
    {
        HomeEntities homeEntities;

        public DataOperations()
        {
            homeEntities = new HomeEntities(); 
        }

        public bool validateLogin(LoginDetails logindetails)
        {

            var password = homeEntities.Logins.Where(a => a.Username == logindetails.username).Select(a => a.Password).SingleOrDefault();
            //var password = (from login in HomeEntities.Logins
            //                where logindetails.username == login.Username
            //                select login.Password).FirstOrDefault();

            if (password == logindetails.password)
            {
                return true;
            }
            return false;
        }

        #region ClassDetails Methods

        public List<ClassDetail> getAllClasses()
        {
            var lstClasses = from classes in homeEntities.ClassDetails where classes.Active == true select classes;

            return lstClasses.ToList();
        }

        public void saveNewClass(ClassDetailViewModel model)
        {
            if (model.id == 0)
            {
                //Add new
                if (model.className != null || model.className != string.Empty)
                {
                    ClassDetail classDetails = new ClassDetail();
                    classDetails.ClassName = model.className;
                    classDetails.Active = true;
                    homeEntities.AddToClassDetails(classDetails);
                    homeEntities.SaveChanges();
                }
            }
            else if(model.id > 0)
            {
                //existing id
                ClassDetail classDetails = getClassById(model.id);
                classDetails.ClassName = model.className;
                homeEntities.SaveChanges();

            }
        }

        public string getClassNameById(int ClassId)
        {
             return homeEntities.ClassDetails.Where(a => a.Id == ClassId && a.Active == true).Select(a=>a.ClassName).FirstOrDefault();

        }

        public ClassDetail getClassById(int ClassId)
        {
            return homeEntities.ClassDetails.Where(a => a.Id == ClassId && a.Active == true).FirstOrDefault();

        }
        #endregion
    }
}