using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdvancedWebForms.Async
{
    public partial class AsyncPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private async Task LoadData()
        {
            await Task.Delay(50);

            results.DataSource = Enumerable.Range(1, 10);

            results.DataBind();
        }
    }
}