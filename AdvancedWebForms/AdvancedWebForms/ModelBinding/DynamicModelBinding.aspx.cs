using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdvancedWebForms.ModelBinding
{
    public partial class DynamicModelBinding : System.Web.UI.Page
    {
        public Widget Model { get; set; }

        protected void Page_Load()
        {
            Model = ModelBindingExecutionContext.BindFromQueryString<Widget>();
        }
    }
}