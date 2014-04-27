using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolAutomationSystem.Areas.Home.Models
{
    public class SubjectMappingViewModel
    {
        DataOperations repository = new DataOperations();
        public int ClassId { get; set; }
        public int DivId { get; set; }
        public int FacultyId { get; set; }
        public int SubjectId { get; set; }
        public SubjectMapping SubjectMapping { get; set; }
        public List<SubjectMappingListData> lstSubjectDetails { get; set; }

        public SelectList GetAllFacultiesSelectList(int selectedValues)
        {
            return repository.GetAllFacultiesSelectList(selectedValues);
        }
        public SelectList GetAllSubjectSelectList(int selectedValues)
        {
            return repository.GetAllSubjectSelectList(selectedValues);
        }
        public SelectList GetAllClassesSelectList(int selectedValue)
        {
            SelectList allClasses = repository.GetAllClassesSelectList(selectedValue);
            return allClasses;

        }
        public SelectList getAllDiv(int selectedValue)
        {
            return repository.GetAllDivSelectList(selectedValue);
        }
    }
    public class SubjectMappingListData
    {
        public int MappingID { get; set; }
        public int ClassId { get; set; }
        public int DivId { get; set; }
        public int FacultyId { get; set; }
        public int SubjectId { get; set; }
        public string FacultyName { get; set; }
        public string SubjectName { get; set; }
        public string IsClassTeacher { get; set; }
    }
    public class JSonReturnData
    {
        public int MappingID { get; set; }
        public int ClassId { get; set; }
        public int DivId { get; set; }
        public int FacultyId { get; set; }
        public int SubjectId { get; set; }
        public bool IsClassTeacher { get; set; }
    }
}