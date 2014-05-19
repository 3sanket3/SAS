using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolAutomationSystem.Models;
using SchoolAutomationSystem.Areas.ResultManagement.Models;

namespace SchoolAutomationSystem.Areas.ResultManagement
{
    public class DataOperations
    {
        HomeEntities homeEntities;
        public SelectListItem[] StaticDropdownItems = new[] { new SelectListItem { Text = "Select One", Value = "0" } };

        public DataOperations()
        {
            homeEntities = new HomeEntities();
        }
        public SelectList getExamList(int selectedValues)
        {
            // var AllDiv = from div in homeEntities.DivDetails select div;
            //ClassStaticDropdownItems

            List<Exam> AllExam = (from exam in homeEntities.Exams
                             
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
        public SelectList GetAllClassesSelectList(int selectedValues)
        {

            List<ClassDetail> lstClasses = (from classes in homeEntities.ClassDetails where classes.Active == true select classes).ToList();

            IEnumerable<SelectListItem> selectListClassses = StaticDropdownItems.Concat(from tempLstClasses in lstClasses
                                                                                        select new SelectListItem
                                                                                        {
                                                                                            Value = tempLstClasses.Id + "",
                                                                                            Text = tempLstClasses.ClassName
                                                                                        });

            SelectList ddlClasses = new SelectList(selectListClassses, "Value", "Text", selectedValues);


            return ddlClasses;



        }

        public SelectList GetAllDivSelectList(int selectedValues)
        {
            // var AllDiv = from div in homeEntities.DivDetails select div;
            //ClassStaticDropdownItems

            List<DivDetail> AllDiv = (from div in homeEntities.DivDetails
                                      select div).ToList();

            IEnumerable<SelectListItem> selectListDiv = StaticDropdownItems.Concat(from tempLstClasses in AllDiv
                                                                                   select new SelectListItem
                                                                                   {
                                                                                       Value = tempLstClasses.Id + "",
                                                                                       Text = tempLstClasses.Div
                                                                                   });

            SelectList ddlDiv = new SelectList(selectListDiv, "Value", "Text", selectedValues);


            return ddlDiv;

            //return new SelectList(AllDiv, "Id", "Div", selectedValues);

        }
        public SelectList GetAllSubjectSelectList(int ClassID,int DivID)
        {

            List<SubjectDetail> lstSubjects = (from subjects in homeEntities.SubjectDetails 
                                               join mappings in homeEntities.SubjectMappings
                                               on subjects.Id equals mappings.SubjectId
                                               where subjects.Active == true && mappings.ClassId == ClassID && mappings.DivId == DivID
                                               select subjects).ToList();

            IEnumerable<SelectListItem> selectListSubject = StaticDropdownItems.Concat(from tempLstFaculties in lstSubjects
                                                                                       select new SelectListItem
                                                                                       {
                                                                                           Value = tempLstFaculties.Id + "",
                                                                                           Text = tempLstFaculties.SubjectName
                                                                                       });

            SelectList ddlSubject = new SelectList(selectListSubject, "Value", "Text");


            return ddlSubject;

        }
        public SelectList GetAllActivitySelectList(int selectedValues)
        {

            List<ActivityDetail> AllActivities = (from activity in homeEntities.ActivityDetails
                                           select activity).ToList();

            IEnumerable<SelectListItem> selectListActivity = StaticDropdownItems.Concat(from tempLstActivities in AllActivities
                                                                                   select new SelectListItem
                                                                                   {
                                                                                       Value = tempLstActivities.ID + "",
                                                                                       Text = tempLstActivities.ActivityName
                                                                                   });

            SelectList ddlActivity = new SelectList(selectListActivity, "Value", "Text", selectedValues);


            return ddlActivity;

            //return new SelectList(AllDiv, "Id", "Div", selectedValues);

        }
        public SelectList GetSubjectsSelectList(int selectedValues)
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
      
        public List<StudentDetail> GetStudentForClassDiv(int ClassID, int DivID)
        {
            List<StudentDetail> Students = (from students in homeEntities.StudentDetails
                                           where students.DivId == DivID && students.ClassId == ClassID
                                           select students).ToList();

            return Students;
        }
        public decimal? GetStudentMarks(BulkResultViewModel model, decimal studentID)
        {
            decimal? marks = 0;
            if (model.ExamID != 5 && model.ExamID != 6)
            {
                marks = (from result in homeEntities.Results
                         where result.ClassID == model.ClassID && result.DivID == model.DivID &&
                         result.ExamID == model.ExamID && result.SubjectID == model.SubjectID
                         && result.StudentID == studentID && result.ActivityID == model.ActivityID
                         select result.Marks).FirstOrDefault();
            }
            else
            {
                marks = (from result in homeEntities.Results
                         where result.ClassID == model.ClassID && result.DivID == model.DivID &&
                         result.ExamID == model.ExamID && result.SubjectID == model.SubjectID
                         && result.StudentID == studentID
                         select result.Marks).FirstOrDefault();
            }
            return marks== null ? 0 : marks;
        }
        public void InsertMarks(string MarksString, string ClassID, string DivID, string ExamID, string SubjectID, string ActivityID)
        {
            MarksString = MarksString.Substring(0, MarksString.Length - 1);
            string[] StudentIDMarks = MarksString.Split(',');
            int intClassID = Convert.ToInt16(ClassID);
            int intDivID = Convert.ToInt16(DivID);
            int intExamID = Convert.ToInt16(ExamID);
            int intSubjectID = Convert.ToInt16(SubjectID);
            Result result;
           int intActivityID = ActivityID != string.Empty ? Convert.ToInt16(ActivityID) : 0;
            foreach (string StudIDMarks in StudentIDMarks)
            {
                string[] IDMarks = StudIDMarks.Split('_');
                int studentID =Convert.ToInt16( IDMarks[1]);
                if (intExamID != 5 && intExamID != 6)
                {
                     result = (from res in homeEntities.Results
                                     where res.ClassID == intClassID && res.DivID == intDivID && res.ExamID == intExamID && res.SubjectID == intSubjectID
                                     && res.StudentID == studentID && intActivityID == res.ActivityID
                                     select res).ToList().FirstOrDefault();
                }
                else
                {
                     result = (from res in homeEntities.Results
                                     where res.ClassID == intClassID && res.DivID == intDivID && res.ExamID == intExamID && res.SubjectID == intSubjectID
                                     && res.StudentID == studentID
                                     select res).ToList().FirstOrDefault();
                }
                if (result != null)
                {
                    result.Marks = Convert.ToInt16(IDMarks[0]);
                    homeEntities.SaveChanges();
                }
                else
                {
                    Result NewResult = new Result();
                    NewResult.ClassID = intClassID;
                    NewResult.DivID = intDivID;
                    NewResult.ExamID = intExamID;
                    NewResult.SubjectID = intSubjectID;
                    NewResult.StudentID = studentID;
                    NewResult.ActivityID = intActivityID;
                    NewResult.Marks = Convert.ToInt16(IDMarks[0]);
                    homeEntities.AddToResults(NewResult);
                    homeEntities.SaveChanges();
                }
                
            }
        }

        internal SelectList GetActivitySelectList(int selectedValues)
        {
            List<ActivityDetail> lstActivity = (from activity in homeEntities.ActivityDetails select activity).ToList();

            IEnumerable<SelectListItem> selectListActivity = StaticDropdownItems.Concat(from tempLstFaculties in lstActivity
                                                                                       select new SelectListItem
                                                                                       {
                                                                                           Value = tempLstFaculties.ID + "",
                                                                                           Text = tempLstFaculties.ActivityName
                                                                                       });

            SelectList ddlActivity = new SelectList(selectListActivity, "Value", "Text", selectedValues);


            return ddlActivity;
        }
    }
}