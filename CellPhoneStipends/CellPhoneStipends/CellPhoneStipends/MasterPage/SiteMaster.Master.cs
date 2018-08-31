using System;

namespace SignageRequestForm.MasterPage
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlNoAccess.Visible = false;
        }
    }
}