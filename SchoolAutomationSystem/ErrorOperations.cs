using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolAutomationSystem.Areas.Home.Models;
using SchoolAutomationSystem.Models;
namespace SchoolAutomationSystem
{
    public class ErrorOperations
    {
         HomeEntities HomeEntities;

         public ErrorOperations()
        {
            HomeEntities = new HomeEntities(); 
        }
         public ErrorDetails LogException(string AppName, Exception ex)
        {
            ErrorLog error = new ErrorLog();
            
            error.AppId = HomeEntities.Applications.Where(a => a.ApplicationName == AppName).Select(a => a.Id).SingleOrDefault();
            error.ExceptionMessage = ex.Message;
            error.AdditionMessage = ex.StackTrace;
            error.CreatedOn = DateTime.Now;
            error.ModifiedOn = DateTime.Now;
            HomeEntities.AddToErrorLogs(error);
            HomeEntities.SaveChanges();
            //error.UserInfo ==

            ErrorDetails errorDetails = new ErrorDetails();
            errorDetails.ApplicationName = AppName;
            errorDetails.ErrorMessage = ex.Message;
            errorDetails.AdditionalInfo = ex.StackTrace;

            return errorDetails;
        }
    }
}