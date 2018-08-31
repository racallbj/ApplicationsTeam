using System;
using System.Linq;
using System.Text;
// ReSharper disable once RedundantUsingDirective 
// Used to get url for email on server
using System.Web; 

namespace CellPhoneStipends.DataLayer
{
    public class CellPhoneStipendEmail
    {
        private static Stipend CellphoneStipend { get; set; }
        private static ConfigValue ConfigValues { get; set; }

        private static string UserName { get; set; }
        private static string UserEmail { get; set; }
        private static string SupervisorEmail { get; set; }
        private static string Body { get; set; }

        public static void EmailStipendRequest(Stipend pCellphoneStipend, string pUserName, string pUserEmail,string pSupervisorEmail)
        {
            UserName = pUserName;
            UserEmail = pUserEmail;
            SupervisorEmail = pSupervisorEmail;
            CellphoneStipend = pCellphoneStipend;
            
            SetConfigValues();

            if(!ConfigValues.Enabled) throw new ApplicationException("Email Currently Disabled");
            
            Body = BuildEmailBody(pCellphoneStipend);
            
            SendEmail();
        }

        
        private static void SetConfigValues()
        {
            var configValues = new EmailConfigValues();

            if (!configValues.Any()) return;

            ConfigValues = configValues[0];
        }

        public static void SendEmail()
        {
            if (!ConfigValues.Enabled) return;
            
            var utility = new VAI.Common.Utility();
            
            var subject = ConfigValues.EmailSubject;

            var toList = !ConfigValues.UseTestEmail ? BuildToList() : ConfigValues.TestEmailTo;

            utility.SendMail(ConfigValues.From, toList, null, subject, Body, ConfigValues.SmtpServer);
        }

        private static string BuildToList()
        {
            var toList = new StringBuilder();

            toList.Append(ConfigValues.EmailTo);
            toList.Append(",");
            toList.Append(UserEmail);
            toList.Append(",");
            toList.Append(SupervisorEmail);
            
            return toList.ToString();
        }
        
        private static string BuildEmailBody(Stipend pCellphoneStipend)
        {
            var rootPath = string.Empty;
            var emailBody = new StringBuilder();
#if !DEBUG
            rootPath = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
#else
            rootPath = "https://webformstest.vai.org/";
#endif
            rootPath = rootPath + "/SignageRequest";
            
            emailBody.Append("<div style='width: 1000px; font-size: 10pt; ");
            emailBody.Append("font-family: \"Helvetica Neue\", \"Lucida Grande\", \"Segoe UI\", Arial, Helvetica, Verdana, sans-serif; color: black;'>");
            
            emailBody.Append("<h2>Van Andel Institute - Cellphone Stipend Request</h2>");

            emailBody.AppendFormat("<p> {0} created a new cellphone stipend request on {1}.", UserName, DateTime.Now.ToString("MM/dd/yyyy"));
            emailBody.Append("<br/><br/> Please review. </p>");

            emailBody.Append(BuildStipendTable(pCellphoneStipend));

            emailBody.Append("</div>");

            return emailBody.ToString();
        }

        private static StringBuilder BuildStipendTable(Stipend pCellphoneStipend)
        {
            var cellphoneStipendTable = new StringBuilder();
            //Supervisor Table
            cellphoneStipendTable.Append(
                "<table style='width: 80%; border: 2px solid black; font-size: 12pt; font-family: \"Helvetica Neue\", \"Lucida Grande\", \"Segoe UI\", Arial, Helvetica, Verdana, sans-serif;'>");
            //Caption
            cellphoneStipendTable.Append("<caption style='font-size: 1.17em; color: black; background-color: transparent; text-decoration:underline; font-style:italic; font-weight:normal; text-align:left;'> ");
            cellphoneStipendTable.Append("Cellphone Stipend Request:</caption>");
            //TableRow Action
            cellphoneStipendTable.Append("<tr> ");

            cellphoneStipendTable.Append("<td style='font-weight: normal; width: 25%; text-align: left; border: 2px solid #D3D3D3;'>Action: </td>");
            cellphoneStipendTable.AppendFormat("<td style='padding: 0; width: 75%; border: 2px solid #D3D3D3;'> {0} </td>",
                pCellphoneStipend.Action);
            cellphoneStipendTable.Append("</tr >");
            //TableRow Employee Name
            cellphoneStipendTable.Append("<tr>");
            cellphoneStipendTable.Append("<td style='font-weight: normal; width: 25%; text-align: left; border: 2px solid #D3D3D3;'> Employee Name: </td>");
            cellphoneStipendTable.AppendFormat("<td style='padding: 0; width: 75%; border: 2px solid #D3D3D3;'> {0} </td>", pCellphoneStipend.EmployeeName);
            cellphoneStipendTable.Append("</tr> ");
            //TableRow Department Lab
            cellphoneStipendTable.Append("<tr>");
            cellphoneStipendTable.Append("<td style='font-weight: normal; width: 25%; text-align: left; border: 2px solid #D3D3D3;'> Department Lab: </td>");
            cellphoneStipendTable.AppendFormat("<td style='padding: 0; width: 75%; border: 2px solid #D3D3D3;'> {0} </td>", pCellphoneStipend.DepartmentLab);
            cellphoneStipendTable.Append("</tr> ");
            //TableRow Supervisor Email
            cellphoneStipendTable.Append("<tr>");
            cellphoneStipendTable.Append("<td style='font-weight: normal; width: 25%; text-align: left; border: 2px solid #D3D3D3;'> Supervisor Email: </td>");
            cellphoneStipendTable.AppendFormat("<td style='padding: 0; width: 75%; border: 2px solid #D3D3D3;'> {0} </td>", pCellphoneStipend.SupervisorEmail);
            cellphoneStipendTable.Append("</tr> ");
            //TableRow Monthly Stipend Level
            cellphoneStipendTable.Append("<tr>");
            cellphoneStipendTable.Append("<td style='font-weight: normal; width: 25%; text-align: left; border: 2px solid #D3D3D3;'> Monthly Stipend Level: </td>");
            cellphoneStipendTable.AppendFormat("<td style='padding: 0; width: 75%; border: 2px solid #D3D3D3;'> {0} </td>", pCellphoneStipend.MonthlyStipendLevel);
            cellphoneStipendTable.Append("</tr> ");
            //TableRow Effective Date
            cellphoneStipendTable.Append("<tr>");
            cellphoneStipendTable.Append("<td style='font-weight: normal; width: 25%; text-align: left; border: 2px solid #D3D3D3;'> Effective Date of Change: </td>");
            cellphoneStipendTable.AppendFormat("<td style='padding: 0; width: 75%; border: 2px solid #D3D3D3;'> {0} </td>", pCellphoneStipend.EffectiveDate.ToString("MM/dd/yyyy"));
            cellphoneStipendTable.Append("</tr> ");
            //End Supervisor Table
            cellphoneStipendTable.Append("</table>");

            return cellphoneStipendTable;
        }
    }
}