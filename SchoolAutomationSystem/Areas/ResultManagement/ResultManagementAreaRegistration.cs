using System.Web.Mvc;

namespace SchoolAutomationSystem.Areas.ResultManagement
{
    public class ResultManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ResultManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ResultManagement_default",
                "ResultManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
