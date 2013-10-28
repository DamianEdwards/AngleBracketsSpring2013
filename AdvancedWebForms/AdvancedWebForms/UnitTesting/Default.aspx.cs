using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdvancedWebForms.UnitTesting
{
    public partial class Default : TestablePage
    {
        // This ctor is called by Web Forms
        public Default()
            : this(null, null, null, null, null)
        {
            
        }

        // Call this ctor from your unit tests passing in your mocks
        public Default(HttpContextBase httpContext, HttpRequestBase httpRequest, HttpResponseBase httpResponse, HttpSessionStateBase httpSessionState, HttpApplicationStateBase httpApplicationState)
            : base(httpContext, httpRequest, httpResponse, httpSessionState, httpApplicationState)
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // This is a UI event handler so we don't want to test it.
            // It just calls into testable UI logic methods that will be unit tested.
            var selectedCategory = GetSelectedCategory();
        }

        public string GetSelectedCategory()
        {
            // This is a view logic method that only interacts with mockable abstractions
            // so can be unit tested

            // Read from the QueryString via abstractions
            var category = HttpRequestBase.QueryString["category"];
            return category;
        }
    }
}