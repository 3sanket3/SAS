using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolAutomationSystem.Areas.Home.Models;
using System.Web.Mvc;

namespace SchoolAutomationSystem.Areas.Home
{
    public class DataOperations
    {
        HomeEntities homeEntities;
        public SelectListItem[] ClassStaticDropdownItems = new[] { new SelectListItem { Text = "Select One", Value = "0" } };
        
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
                classDetails.Active = true;
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

        #region DivDetails Methods

        public List<DivDetail> getAllDiv(int classID)
        {
          //  int intClassID = Convert.ToInt32(classID);
            return (homeEntities.DivDetails.Where(b => b.Active == true && b.ClassId == classID)).ToList();
        }
        public SelectList getAllClassesInDropDownList()
        {
            List<ClassDetail> lstClasses = (from classes in homeEntities.ClassDetails where classes.Active == true select classes).ToList();

            IEnumerable<SelectListItem> selectListClassses = ClassStaticDropdownItems.Concat( from tempLstClasses in lstClasses
                                                             select new SelectListItem
                                                             {
                                                                 Value = tempLstClasses.Id+"",
                                                                 Text = tempLstClasses.ClassName
                                                             });

            SelectList ddlClasses = new SelectList(selectListClassses, "Value", "Text");
            

            return ddlClasses;
        }

        public void SaveDivDetails(DivDetailViewModel model)
        {
            if (model.divId == 0 && !string.IsNullOrEmpty(model.divName))
            {
                //Add New
                DivDetail DivDetails = new DivDetail();
                DivDetails.Div = model.divName;
                DivDetails.ClassId = model.classID;
                DivDetails.Active = true;
                homeEntities.AddToDivDetails(DivDetails);
                homeEntities.SaveChanges();

            }
            else if(model.divId > 0 && !string.IsNullOrEmpty(model.divName))
            {
                //Update Existing
                DivDetail DivDetails = homeEntities.DivDetails.Where(a => a.Id == model.divId && a.Active == true).FirstOrDefault();

                if (DivDetails != null)
                {
                    DivDetails.Div = model.divName;
                    DivDetails.ClassId = model.classID;
                    homeEntities.SaveChanges();
                }
            }
        }

        public string getDivNameById(int DivId)
        {
            return homeEntities.DivDetails.Where(a => a.Id == DivId && a.Active == true).Select(a => a.Div).FirstOrDefault();

        }
        #endregion

        #region Subjects Methods
        public List<SubjectDetail> getAllSubjects()
        {
            return homeEntities.SubjectDetails.Where(a => a.Active == true).ToList();
        }

        public void saveNewSubject(SubjectDetailViewModel model)
        {
            if (model.ID == 0)
            {
                //Add new
                if (model.SubjectName != null || model.SubjectName != string.Empty)
                {
                    SubjectDetail SubjectDetails = new SubjectDetail();
                    SubjectDetails.SubjectName = model.SubjectName;
                    SubjectDetails.Active = true;
                    homeEntities.AddToSubjectDetails(SubjectDetails);
                    homeEntities.SaveChanges();
                }
            }
            else if (model.ID > 0)
            {
                //existing id
                SubjectDetail SubjectDetails = getSubjectDetailsFromID(model.ID);
                SubjectDetails.SubjectName = model.SubjectName;
                SubjectDetails.Active = true;
                homeEntities.SaveChanges();

            }
        }

        private SubjectDetail getSubjectDetailsFromID(int id)
        {
            return homeEntities.SubjectDetails.Where(a => a.Id == id).Select(a => a).First();
        }


        public string getSubjectNameById(int SubjectID)
        {
            return homeEntities.SubjectDetails.Where(a => a.Id == SubjectID && a.Active == true).Select(a => a.SubjectName).FirstOrDefault();

        }
        #endregion

        #region Faculty Methos

        internal List<FacultyDetail> getFacultyDetailsList()
        {
            List<FacultyDetail> FacultyDetails = new List<FacultyDetail>();

            FacultyDetails = (from faculties in homeEntities.FacultyDetails
                              select faculties).ToList();

            return FacultyDetails;
        }

        internal FacultyDetail getFacultyDetailFromID(int ID)
        {
            return (from faculty in homeEntities.FacultyDetails
                    where faculty.FacultyId == ID
                    select faculty).First();

        }

        internal void SaveFacultyDetails(FacultyDetail model)
        {
            FacultyDetail faculty;
            if (model.FacultyId > 0)
            {
                //Update Faculty Details
                 faculty= (from facultyDetails in homeEntities.FacultyDetails
                                         where facultyDetails.FacultyId == model.FacultyId
                                         select facultyDetails).FirstOrDefault();
                 if (faculty != null)
                 {
                     CopyFacultyDetails(model, faculty);
                 }
            }
            else
            {
                faculty = new FacultyDetail();
                CopyFacultyDetails(model, faculty);
                homeEntities.AddToFacultyDetails(faculty);
            }
            homeEntities.SaveChanges();
            
        }

        public void CopyFacultyDetails(FacultyDetail source, FacultyDetail destination)
        {
            destination.FacultyUniqueName = source.FacultyUniqueName;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.MiddleName = source.MiddleName;
            destination.Password = source.Password;
            destination.PhoneNo = source.PhoneNo;
            destination.Address = source.Address;
            destination.BirthDate = source.BirthDate;
            destination.BloodGroup = source.BloodGroup;
            destination.EmailId = source.EmailId;
            destination.Active = source.Active;
        }

        public List<FacultyDetail> SearchFaculties(FacultyDetail faculty)
        {
            List<FacultyDetail> lstAllFaculties = (from faculties in homeEntities.FacultyDetails
                                                   where faculties.Active == true
                                                   select faculties).ToList();

            if (!string.IsNullOrEmpty(faculty.FirstName))
            {
                lstAllFaculties = (from faculties in lstAllFaculties
                                   where faculties.FirstName != null && faculties.FirstName.StartsWith(faculty.FirstName)
                                   select faculties).ToList();
            }
            if (!string.IsNullOrEmpty(faculty.LastName))
            {
                lstAllFaculties = (from faculties in lstAllFaculties
                                   where faculties.LastName != null && faculties.LastName.StartsWith(faculty.LastName)
                                   select faculties).ToList();
            }
            if (!string.IsNullOrEmpty(faculty.FacultyUniqueName))
            {
                lstAllFaculties = (from faculties in lstAllFaculties
                                   where faculties.FacultyUniqueName != null && faculties.FacultyUniqueName.StartsWith(faculty.FacultyUniqueName)
                                   select faculties).ToList();
            }
            if (!string.IsNullOrEmpty(faculty.EmailId))
            {
                lstAllFaculties = (from faculties in lstAllFaculties
                                   where faculties.EmailId != null && faculties.EmailId.StartsWith(faculty.EmailId)
                                   select faculties).ToList();
            }
            return lstAllFaculties;
        }
        #endregion

        #region Student Methos

        internal List<StudentDetail> getStudentDetailsList()
        {
            List<StudentDetail> StudetnDetails = new List<StudentDetail>();

            StudetnDetails = (from studetns in homeEntities.StudentDetails
                              select studetns).ToList();

            return StudetnDetails;
        }

        //internal FacultyDetail getFacultyDetailFromID(int ID)
        //{
        //    return (from faculty in homeEntities.FacultyDetails
        //            where faculty.FacultyId == ID
        //            select faculty).First();

        //}

        internal void SaveFacultyDetails(StudentDetail model)
        {
            StudentDetail Student;
            if (model.Id > 0)
            {
                //Update Faculty Details
                Student = (from studentDetails in homeEntities.StudentDetails
                           where studentDetails.Id == model.Id
                           select studentDetails).FirstOrDefault();
                if (Student != null)
                {
                    CopyStudentDetails(model, Student);
                }
            }
            else
            {
                Student= new StudentDetail();
                CopyStudentDetails(model, Student);
                homeEntities.AddToStudentDetails(Student);
            }
            homeEntities.SaveChanges();

        }

        public void CopyStudentDetails(StudentDetail source, StudentDetail destination)
        {

            destination.FirstName = source.FirstName;
            destination.GRNo = source.GRNo;
            destination.RollNo = source.RollNo;
            destination.ClassId = source.DivId;
            destination.LastName = source.LastName;
            destination.MiddleName = source.MiddleName;
            destination.PhoneNo = source.PhoneNo;
            destination.Address = source.Address;
            destination.BirthDate = source.BirthDate;
            destination.BloodGroup = source.BloodGroup;
            destination.PFName = source.PFName;
            destination.PMName = source.PMName;
            destination.PLName = source.PLName;
            destination.Active = source.Active;
        }

        public List<StudentDetail> SearchStudents(SearchStudentData searchStudentDetails)
        {
            List<StudentDetail> lstAllStudents = (from students in homeEntities.StudentDetails
                                                  where students.Active == true
                                                  select students).ToList();

            if (!string.IsNullOrEmpty(searchStudentDetails.StudentFirstName))
            {
                lstAllStudents = (from students in lstAllStudents
                                  where students.FirstName != null && students.FirstName.StartsWith(searchStudentDetails.StudentFirstName)
                                  select students).ToList();
            }
            if (!string.IsNullOrEmpty(searchStudentDetails.StudentLastName))
            {
                lstAllStudents = (from students in lstAllStudents
                                  where students.LastName != null && students.LastName.StartsWith(searchStudentDetails.StudentLastName)
                                  select students).ToList();
            }
            if (!string.IsNullOrEmpty(searchStudentDetails.GRNO.ToString()))
            {
                lstAllStudents = (from students in lstAllStudents
                                  where students.GRNo != null && students.GRNo== searchStudentDetails.GRNO
                                  select students).ToList();
            }
            if (!string.IsNullOrEmpty(searchStudentDetails.RollNo.ToString()))
            {
                lstAllStudents = (from students in lstAllStudents
                                  where students.RollNo != null && students.RollNo == searchStudentDetails.RollNo
                                  select students).ToList();
            }
            return lstAllStudents;
        }
        #endregion
    }
}