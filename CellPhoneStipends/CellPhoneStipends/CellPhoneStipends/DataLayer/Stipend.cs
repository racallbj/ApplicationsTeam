using System;
using System.Dynamic;

namespace CellPhoneStipends.DataLayer
{
    public class Stipend
    {
        public int StipendID { get; private set; }
        public string Action { get; private set; }
        public string EmployeeName { get; private set; }
        public string DepartmentLab { get; private set; }
        public DateTime EffectiveDate { get; private set; }
        public string MonthlyStipendLevel { get; private set; }

        public string SupervisorEmail { get; set; }

        public Stipend(int pStipendID, string pAction, string pEmployeeName, DateTime pEffectiveDate, string pMonthlyStipendLevel, string pSupervisorEmail, string pDepartmentLab = null)
        {
            StipendID = pStipendID;
            Action = pAction;
            EmployeeName = pEmployeeName;
            DepartmentLab = pDepartmentLab;
            SupervisorEmail = pSupervisorEmail;
            EffectiveDate = pEffectiveDate;
            MonthlyStipendLevel = pMonthlyStipendLevel;
        }
    }
}