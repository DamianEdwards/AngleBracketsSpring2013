using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;

namespace AdvancedWebForms.ModelBinding
{
    public partial class AdHocModelBinding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            var category = new Widget();
            
            var valueProvider = new FormValueProvider(ModelBindingExecutionContext);
            //var valueProvider = new AggregateValueProvider(ModelBindingExecutionContext);
            
            TryUpdateModel(category, valueProvider);
            
            if (ModelState.IsValid)
            {
                Response.Redirect(FriendlyUrl.Resolve("~/ModelBinding/Success.aspx"));
            }
        }
    }
}