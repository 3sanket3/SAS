using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolAutomationSystem.Areas.Home.Models;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using SchoolAutomationSystem.Models;

namespace SchoolAutomationSystem.Areas.Home
{
    public class DataOperations
    {
        HomeEntities homeEntities;
        public SelectListItem[] StaticDropdownItems = new[] { new SelectListItem { Text = "Select One", Value = "0" } };
        
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

        public bool IsClassExist(string className)
        {
            ClassDetail lstClasses = (from classes in homeEntities.ClassDetails where classes.Active == true  && classes.ClassName == className select classes).FirstOrDefault();

            return lstClasses != null ? true : false;
        }
        public bool IsDivExist(int classID,string divName)
        {
            DivDetail lstDiv = (from div in homeEntities.DivDetails where div.ClassId == classID && div.Div == divName && div.Active == true select div).FirstOrDefault();
            return lstDiv != null ? true : false;
        }
        public bool IsSubjectExist(string subject)
        {
            SubjectDetail subjectdetails = (from sub in homeEntities.SubjectDetails where sub.Active == true && sub.SubjectName == subject select sub).FirstOrDefault();

            return subjectdetails != null ? true : false;
        }
        public bool IsFacultyExist(int FacultyID,string shortName)
        {
            FacultyDetail facultyDetails = (from faculty in homeEntities.FacultyDetails where faculty.Active == true && faculty.FacultyUniqueName == shortName select faculty).FirstOrDefault();
            return facultyDetails != null ? (facultyDetails.FacultyId == FacultyID ? false : true)  : false;
        }
        public bool IsStudentExist(StudentDetail studentDetails, out string successerrorMsg)
        {
            StudentDetail student = (from stu in homeEntities.StudentDetails
                                     where stu.Active == true && stu.GRNo == studentDetails.GRNo
                                     select stu).FirstOrDefault();
            if (student != null && studentDetails.Id != student.Id)
            {
                successerrorMsg = "<div id='SuccessErrorMessage' style = 'color : Red'> Student with GR NO '"+ studentDetails.GRNo+"' is already present </div>";
                return true;
            }
            else
            {
                student = (from stu in homeEntities.StudentDetails
                           where stu.Active == true && stu.RollNo == studentDetails.RollNo && stu.DivId == studentDetails.DivId
                               && stu.ClassId == studentDetails.ClassId
                           select stu).FirstOrDefault();
                successerrorMsg = "<div id='SuccessErrorMessage' style = 'color : Red'> Student with Roll No'" + studentDetails.RollNo + "' is already present in '" + getClassNameById((int)studentDetails.ClassId) + " " + getDivNameById((int)studentDetails.DivId) + "'</div>";

                return student != null ? (student.Id == studentDetails.Id ? false : true) : false;
                
            }
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

            IEnumerable<SelectListItem> selectListClassses = StaticDropdownItems.Concat( from tempLstClasses in lstClasses
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
                    SubjectDetails.SubjectName = model.SubjectName.Trim();
                    SubjectDetails.Active = true;
                    homeEntities.AddToSubjectDetails(SubjectDetails);
                    homeEntities.SaveChanges();
                }
            }
            else if (model.ID > 0)
            {
                //existing id
                SubjectDetail SubjectDetails = getSubjectDetailsFromID(model.ID);
                SubjectDetails.SubjectName = model.SubjectName.Trim();
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

        public SelectList GetAllBloodGroupSelectList(int selectedValues)
        {
            // var AllDiv = from div in homeEntities.DivDetails select div;
            //ClassStaticDropdownItems

            List<BloodGroup> AllBloodGroup = (from bloodGroup in homeEntities.BloodGroups
                                             select bloodGroup).ToList();

            IEnumerable<SelectListItem> selectListgroup = StaticDropdownItems.Concat(from tempLstgroup in AllBloodGroup
                                                                                   select new SelectListItem
                                                                                   {
                                                                                       Value = tempLstgroup.ID + "",
                                                                                       Text = tempLstgroup.BloodGroup1
                                                                                   });

            SelectList ddlBloodGroup = new SelectList(selectListgroup, "Value", "Text", selectedValues);


            return ddlBloodGroup;

            //return new SelectList(AllDiv, "Id", "Div", selectedValues);

        }

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
            destination.FacultyUniqueName = source.FacultyUniqueName.Trim();
            destination.FirstName = source.FirstName.Trim();
            destination.LastName = source.LastName.Trim();
        //    destination.MiddleName = source.MiddleName.Trim();
            destination.Password = source.Password;
            destination.PhoneNo = source.PhoneNo;
            destination.Address = source.Address;
            destination.BirthDate = source.BirthDate;
            destination.BloodGroup = source.BloodGroup;
            destination.EmailId = source.EmailId;
            destination.Active = true;
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

        internal StudentDetail getStudentDetailFromID(int ID)
        {
            return (from student in homeEntities.StudentDetails
                    where student.Id == ID
                    select student).First();
            
        }

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
            destination.ClassId = source.ClassId;
            destination.DivId = source.DivId;
            destination.LastName = source.LastName;
            destination.MiddleName = source.MiddleName;
            destination.PhoneNo = source.PhoneNo;
            destination.Address = source.Address;
            destination.BirthDate = source.BirthDate;
            destination.BloodGroup = source.BloodGroup;
            destination.PFName = source.PFName;
            destination.PMName = source.PMName;
            destination.PLName = source.PLName;
            destination.Active = true;
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
            if (!string.IsNullOrEmpty(searchStudentDetails.GRNO.ToString()) && searchStudentDetails.GRNO >0)
            {
                lstAllStudents = (from students in lstAllStudents
                                  where students.GRNo != null && students.GRNo== searchStudentDetails.GRNO
                                  select students).ToList();
            }
            if (!string.IsNullOrEmpty(searchStudentDetails.RollNo.ToString()) && searchStudentDetails.RollNo >0)
            {
                lstAllStudents = (from students in lstAllStudents
                                  where students.RollNo != null && students.RollNo == searchStudentDetails.RollNo
                                  select students).ToList();
            }
            if (searchStudentDetails.ClassId >0)
            {
                lstAllStudents = (from students in lstAllStudents
                                  where students.ClassId != null && students.ClassId == searchStudentDetails.ClassId
                                  select students).ToList();
            }
            if (searchStudentDetails.DivId > 0)
            {
                lstAllStudents = (from students in lstAllStudents
                                  where students.DivId != null && students.DivId == searchStudentDetails.DivId
                                  select students).ToList();
            }
            return lstAllStudents;
        }

        public SelectList GetAllClassesSelectList(int selectedValues)
        {
           
            List<ClassDetail> lstClasses = (from classes in homeEntities.ClassDetails where classes.Active == true select classes).ToList();

            IEnumerable<SelectListItem> selectListClassses = StaticDropdownItems.Concat( from tempLstClasses in lstClasses
                                                             select new SelectListItem
                                                             {
                                                                 Value = tempLstClasses.Id+"",
                                                                 Text = tempLstClasses.ClassName
                                                             });

            SelectList ddlClasses = new SelectList(selectListClassses, "Value", "Text",selectedValues);
            

            return ddlClasses;
        


        }

        public SelectList GetAllDivSelectList(int selectedValues)
        {
           // var AllDiv = from div in homeEntities.DivDetails select div;
            //ClassStaticDropdownItems

                 List<DivDetail> AllDiv = (from div in homeEntities.DivDetails
                           select div).ToList();

            IEnumerable<SelectListItem> selectListDiv = StaticDropdownItems.Concat( from tempLstClasses in AllDiv
                                                             select new SelectListItem
                                                             {
                                                                 Value = tempLstClasses.Id+"",
                                                                 Text = tempLstClasses.Div
                                                             });

            SelectList ddlDiv = new SelectList(selectListDiv, "Value", "Text", selectedValues);


            return ddlDiv;

            //return new SelectList(AllDiv, "Id", "Div", selectedValues);

        }
        #endregion

        #region SubjectMapping

        public List<SubjectMappingListData> getAllSubjectMapping(SubjectMapping mapping)
        {
            List<SubjectMappingListData> MappingListData = (from list in homeEntities.SubjectMappings
                                                           join faculty in homeEntities.FacultyDetails
                                                           on list.FacultyId equals faculty.FacultyId
                                                           join subject in homeEntities.SubjectDetails
                                                           on list.SubjectId equals subject.Id
                                                           where list.Active == true 
                                                           && list.ClassId == mapping.ClassId
                                                           && list.DivId== mapping.DivId
                                                           select new SubjectMappingListData
                                                           {
                                                               MappingID = (int)list.Id,
                                                               ClassId = (int)list.ClassId,
                                                               DivId = (int)list.DivId,
                                                               FacultyId = (int)list.FacultyId,
                                                               FacultyName = faculty.FirstName + faculty.LastName,
                                                               SubjectId = (int)list.SubjectId,
                                                               SubjectName = subject.SubjectName,
                                                               IsClassTeacher = list.IsClassTeacher==true? "Yes" : "No"
                                                               
                                                           }).ToList();

            return MappingListData;
        }

        public SelectList GetAllFacultiesSelectList(int selectedValues)
        {

            List<FacultyDetail> lstFaculties = (from faculties in homeEntities.FacultyDetails where faculties.Active == true select faculties).ToList();

            IEnumerable<SelectListItem> selectListFaculties = StaticDropdownItems.Concat(from tempLstFaculties in lstFaculties
                                                                                        select new SelectListItem
                                                                                        {
                                                                                            Value = tempLstFaculties.FacultyId + "",
                                                                                            Text = tempLstFaculties.FirstName + " "+tempLstFaculties.LastName
                                                                                        });

            SelectList ddlFaculties = new SelectList(selectListFaculties, "Value", "Text", selectedValues);


            return ddlFaculties;

        }

        public SelectList GetAllSubjectSelectList(int selectedValues)
        {

            List<SubjectDetail> lstSubjects = (from subjects in homeEntities.SubjectDetails where subjects.Active == true select subjects).ToList();

            IEnumerable<SelectListItem> selectListSubject = StaticDropdownItems.Concat(from tempLstFaculties in lstSubjects
                                                                                         select new SelectListItem
                                                                                         {
                                                                                             Value = tempLstFaculties.Id + "",
                                                                                             Text = tempLstFaculties.SubjectName
                                                                                         });

            SelectList ddlSubject = new SelectList(selectListSubject, "Value", "Text", selectedValues);


            return ddlSubject;

        }

        public void SaveMapping(SubjectMappingViewModel model)
        {
            SubjectMapping SubMapping;
           if (model.SubjectMapping.Id == 0)
            {
                ///Add New
                SubMapping = new SubjectMapping();
                SubMapping.ClassId = model.SubjectMapping.ClassId;
                SubMapping.DivId = model.SubjectMapping.DivId;
                SubMapping.FacultyId = model.SubjectMapping.FacultyId;
                SubMapping.SubjectId = model.SubjectMapping.SubjectId;
                SubMapping.IsClassTeacher = model.SubjectMapping.IsClassTeacher;
                SubMapping.Active = true;
                homeEntities.AddToSubjectMappings(SubMapping);

            }
            else
            {
                //update
                SubMapping = (from mapping in homeEntities.SubjectMappings
                              where mapping.Id == model.SubjectMapping.Id
                              select mapping).FirstOrDefault();
                SubMapping.ClassId = model.SubjectMapping.ClassId;
                SubMapping.DivId = model.SubjectMapping.DivId;
                SubMapping.FacultyId = model.SubjectMapping.FacultyId;
                SubMapping.SubjectId = model.SubjectMapping.SubjectId;
                SubMapping.IsClassTeacher = model.SubjectMapping.IsClassTeacher;
                
            }
           homeEntities.SaveChanges();
        }

        public JSonReturnData GetSubjectMappingDetails(int MappingDetails)
        {
           SubjectMapping SubMapping = (from mapping in homeEntities.SubjectMappings
                                        where mapping.Id == MappingDetails
                          select mapping).FirstOrDefault();
           JSonReturnData jsonData = new JSonReturnData();
           jsonData.MappingID = (int)SubMapping.Id;
           jsonData.SubjectId = (int)SubMapping.SubjectId;
           jsonData.DivId = (int)SubMapping.DivId;
           jsonData.ClassId= (int)SubMapping.ClassId;
           jsonData.FacultyId = (int)SubMapping.FacultyId;
           jsonData.IsClassTeacher = SubMapping.IsClassTeacher == true ? true : false;


           //JSonReturnData jsonData = new JSonReturnData();
           //jsonData.MappingID = (int)SubMapping.Id;
           //jsonData.SubjectId = getSubjectNameById((int)SubMapping.SubjectId);
           //jsonData.DivId = getDivNameById((int)SubMapping.DivId);
           //jsonData.ClassId = getClassNameById((int)SubMapping.ClassId);
           //jsonData.FacultyId = getFacultyDetailFromID((int)SubMapping.FacultyId).FirstName + " " + getFacultyDetailFromID((int)SubMapping.FacultyId).LastName;
           //jsonData.IsClassTeacher = SubMapping.IsClassTeacher == true ? true : false;

           return jsonData;
        }

        public string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }  
        #endregion
        #region Activity

        public SelectList getFExamList(int selectedValues)
        {
            // var AllDiv = from div in homeEntities.DivDetails select div;
            //ClassStaticDropdownItems

            List<Exam> AllExam = (from exam  in homeEntities.Exams
                                  where exam.Type == "F"
                                  select exam).ToList();

            IEnumerable<SelectListItem> selectListExam = StaticDropdownItems.Concat(from tempLstExam in AllExam
                                                                                   select new SelectListItem
                                                                                   {
                                                                                       Value = tempLstExam.ID + "",
                                                                                       Text = tempLstExam.ExamName
                                                                                   });

            SelectList ddlExam = new SelectList(selectListExam, "Value", "Text", selectedValues);


            return ddlExam;

            //return new SelectList(AllDiv, "Id", "Div", selectedValues);

        }


        public List<ActivityDetail> getAllActivity(ActivityDetail activityDetails)
        {
            return (from activity in homeEntities.ActivityDetails
                    where activity.ClassID == activityDetails.ClassID
                    && activity.DivID == activityDetails.DivID
                    && activity.ExamID == activityDetails.ExamID
                    && activity.SubjectID == activityDetails.SubjectID
                    select activity).ToList();

        }

        public void SaveActivity(ActivityDetailsViewModel model)
        {
            ActivityDetail ActivityDetails;
            if (model.ActivityDetail.ID == 0)
            {
                ActivityDetails = new ActivityDetail();
                ActivityDetails.ClassID = model.ActivityDetail.ClassID;
                ActivityDetails.DivID = model.ActivityDetail.DivID;
                ActivityDetails.ExamID = model.ActivityDetail.ExamID;
                ActivityDetails.SubjectID = model.ActivityDetail.SubjectID;
                ActivityDetails.Date = model.ActivityDetail.Date;
                ActivityDetails.TotalMarks = model.ActivityDetail.TotalMarks;
                ActivityDetails.ActivityName = model.ActivityDetail.ActivityName;
                homeEntities.AddToActivityDetails(ActivityDetails);
            }
            else
            {
                ActivityDetails = (from activity in homeEntities.ActivityDetails
                                   where activity.ID == model.ActivityDetail.ID
                                   select activity).FirstOrDefault();
                ActivityDetails.ClassID = model.ActivityDetail.ClassID;
                ActivityDetails.DivID = model.ActivityDetail.DivID;
                ActivityDetails.ExamID = model.ActivityDetail.ExamID;
                ActivityDetails.SubjectID = model.ActivityDetail.SubjectID;
                ActivityDetails.Date = model.ActivityDetail.Date;
                ActivityDetails.TotalMarks = model.ActivityDetail.TotalMarks;
                ActivityDetails.ActivityName = model.ActivityDetail.ActivityName;

            }
            homeEntities.SaveChanges();
        }

        public JSonReturnActivityData GetActivityDetails(int ActivityID)
        {
            ActivityDetail ActivityDetails = (from activity in homeEntities.ActivityDetails
                                              where activity.ID == ActivityID
                                              select activity).FirstOrDefault();

            JSonReturnActivityData jsonData = new JSonReturnActivityData();
            jsonData.ActivityID = (int)ActivityDetails.ID;
            jsonData.ClassID = (int)ActivityDetails.ClassID;
            jsonData.DivID = (int)ActivityDetails.DivID;
            jsonData.ExamID = (int)ActivityDetails.ExamID;
            jsonData.SubjectID = (int)ActivityDetails.SubjectID;
            jsonData.TotalMarks = ActivityDetails.TotalMarks == null ? 0 : (int)ActivityDetails.TotalMarks;
            jsonData.ActivityName = ActivityDetails.ActivityName == null ? string.Empty : ActivityDetails.ActivityName;
            jsonData.date = ActivityDetails.Date == null ? string.Empty : ActivityDetails.Date.Value.Date.ToShortDateString();
            
            return jsonData;
        }
        #endregion
    }
}