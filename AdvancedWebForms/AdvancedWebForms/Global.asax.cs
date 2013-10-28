using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using AdvancedWebForms.Backgrounder;
using AdvancedWebForms.ModelBinding;
using WebBackgrounder;

namespace AdvancedWebForms
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BackgrounderConfig.Start();

            ModelBinderProviders.Providers.Insert(ModelBinderProviders.Providers.Count - 1, new ImmutableObjectModelBinderProvider());
        }

        protected void Application_End(object sender, EventArgs e)
        {
            BackgrounderConfig.Stop();
        }
    }
}