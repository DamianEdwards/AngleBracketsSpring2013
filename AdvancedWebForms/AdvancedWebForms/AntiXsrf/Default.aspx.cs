using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdvancedWebForms.AntiXsrf
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Transfer_Click(object sender, EventArgs e)
        {
            // Do money transfer here...
            Success.Visible = true;
            SuccessMessage.Text = String.Format(
                "${0:d} transferred from {1} to {2}-{3} successfully!",
                amount.Text,
                fromAccount.Text,
                toAccountRoutingNumber.Text,
                toAccountNumber.Text);
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }
    }
}