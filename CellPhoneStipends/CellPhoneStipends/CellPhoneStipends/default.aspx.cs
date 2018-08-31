using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CellPhoneStipends.DataLayer;

using System.Data;
using System.Configuration;
using System.Data.SqlClient;

using VAI.Common;

namespace CellPhoneStipends
{
    public partial class _default : Page
    {
        #region Properties

        private string UserEmail
        {
            get { return ViewState["UserEmail"] != null ? ViewState["UserEmail"].ToString() : null; }
            set { ViewState["UserEmail"] = value; }
        }

        private string UserName
        {
            get { return ViewState["UserName"] != null ? ViewState["UserName"].ToString() : null; }
            set { ViewState["UserName"] = value; }
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            
            SetUserEmail();
            LoadDropdowns();
        }

        private void SetUserEmail()
        {
            string userEmail = null;

            if (Request.LogonUserIdentity != null)
            {
                UserName = Request.LogonUserIdentity.Name.Split(Convert.ToChar(@"\"))[1];            
                userEmail = GetEmployeeEmail(UserName);
            }

            UserEmail = userEmail;
        }

        private void LoadDropdowns()
        {
            var levels = CellphoneStipendDataSet.GetStipendLevels();
            var types = CellphoneStipendDataSet.GetStipendTypes();

            ddlAction.DataSource = types;
            ddlAction.DataBind();
            ddlAction.Items.Insert(0, new ListItem("Select..."));
            
            ddlMonthlyStipendLevel.DataSource = levels;
            ddlMonthlyStipendLevel.DataBind();
            ddlMonthlyStipendLevel.Items.Insert(0,new ListItem("Select..."));
        }

        #region Save

        protected void btnSave_Click(object pSender, EventArgs pE)
        {
            if(!Page.IsValid || !CanSave()) return;
            
            var stipend = GetStipend();
            
            CellPhoneStipendEmail.EmailStipendRequest(stipend,UserName,UserEmail,txtSupervisorEmail.Value.ToString());

            Response.Redirect("success.htm");
        }

        private Stipend GetStipend()
        {
            const int id = 0;
            var action = ddlAction.SelectedValue;
            var name = txtEmployeeName.Value.Trim();
            
            var departmentLab = txtDepartmentLab.Value.Trim();

            var date = DateTime.Parse(txtEffectiveDate.Text);

            var level = ddlMonthlyStipendLevel.SelectedItem.Text;

            var supervisorEmail = txtSupervisorEmail.Value.Trim();

            var stipend = new Stipend(pStipendID: id,pAction: action,pEmployeeName: name,pEffectiveDate: date,pMonthlyStipendLevel: level, pSupervisorEmail: supervisorEmail ,pDepartmentLab: departmentLab);

            return stipend;
        }
        
        protected bool CanSave()
        {
            return UserName != null;
        }

        #endregion
        
        #region Cancel
        protected void btnCancel_Click(object pSender, EventArgs pE)
        {
           Response.Redirect("default.aspx");
        }

        #endregion

        #region Database

        public string GetEmployeeEmail(string adUserID)
        {
            SqlConnection conn = null;
            string result = string.Empty;

            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                SqlParameter pADUserID = new SqlParameter("@ADUserID", SqlDbType.VarChar, 256);
                pADUserID.Value = adUserID;

                SqlParameter pEmailAddress = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 60);
                pEmailAddress.Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetEmployeeEmail";
                cmd.Parameters.Add(pADUserID);
                cmd.Parameters.Add(pEmailAddress);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                result = pEmailAddress.Value.ToString();

                if (string.IsNullOrEmpty(result))
                    result = "Jim.Clinthorne@vai.org";

                return pEmailAddress.Value.ToString();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        #endregion

    }
}