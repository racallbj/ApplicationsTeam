using System.Data;
using CellPhoneStipends.DataLayer.CellphoneStipendDataSetTableAdapters;

namespace CellPhoneStipends.DataLayer {
    
    public partial class CellphoneStipendDataSet {

        public static DataTable GetConfigValues()
        {
            var dt = new EmailConfigValuesDataTable();
            var dta = new EmailConfigValuesTableAdapter();
            dta.Fill(dt);

            return dt;
        }


        public static DataTable GetStipendLevels()
        {
            var dt = new StipendLevelsDataTable();
            var dta = new StipendLevelsTableAdapter();

            dta.Fill(dt);

            return dt;
        }

        public static DataTable GetStipendTypes()
        {
            var dt = new StipendTypesDataTable();
            var dta = new StipendTypesTableAdapter();

            dta.Fill(dt);

            return dt;
        } 
    }
}
