using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SAS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public string GetClassDdetails(int ClasID)
        {
            string str = "";
            return str;
        }


        internal string getFacultyDetailsList()
        {
            string str = "";
            return str;

        }

        public string getAllActivity(ActivityDetail activityDetails)
        {
            string str = "";
            return str;
        }


        internal string getStudentDetailFromID(int ID)
        {
            string str = "";
            return str;
        }

        internal string getStudentDetailsList()
        {
            string str = "";
            return str;
        }

        private string getSubjectDetailsFromID(int id)
        {
            string str = "";
            return str;
        }



        public string getSubjectNameById(int SubjectID)
        {
            string str="";
            return str;
        }
        public string GetDivDetails(int DivID)
        {
            string str = "";
            return str;
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
