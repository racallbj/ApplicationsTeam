using System;
using System.Collections.Generic;
using System.Data;

namespace CellPhoneStipends.DataLayer
{
    public class ConfigValue
    {
        public bool Enabled { get; private set; }
        public string SmtpServer { get; private set; }
        public string From { get; private set; }
        public string EmailSubject { get; private set; }
        public string TestEmailTo { get; set; }
        public string EmailTo { get; private set; }
        public string Body { get; set; }
        public bool UseTestEmail { get; set; }

        
        
        public ConfigValue(DataRow pRow)
        {
            Body = pRow["EmailBody"].ToString();

            Enabled = Convert.ToBoolean(pRow["EmailEnabled"].ToString());

            From = pRow["EmailFrom"].ToString();

            EmailSubject = pRow["EmailSubject"].ToString();

            EmailTo = pRow["EmailTo"].ToString();

            SmtpServer = pRow["SmptServer"].ToString();
            
            TestEmailTo = pRow["TestEmailTo"].ToString();
            
            UseTestEmail = bool.Parse(pRow["UseTestEmail"].ToString());
        }
    }

    public class EmailConfigValues : List<ConfigValue>
    {
        public EmailConfigValues()
        {
            var configValues = CellphoneStipendDataSet.GetConfigValues();

            foreach (DataRow row in configValues.Rows)
            {
                var config = new ConfigValue(row);
                Add(config);
            }
        }
    }
}