using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdvancedWebForms.Async
{
    public partial class AsyncUI_Content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            uint viewIndex = 0;
            uint.TryParse(Request.QueryString["view"], out viewIndex);

            views.ActiveViewIndex = (int)(viewIndex < views.Views.Count ? viewIndex : 0);
        }

        public IEnumerable Grid1_GetData()
        {
            // Fake long DB call, never sleep in ASP.NET :)
            Thread.Sleep(TimeSpan.FromSeconds(2));

            return new[] {
                new { FirstName = "Damian", LastName="Edwards", Title="Senior Program Manager" },
                new { FirstName = "Levi", LastName="Broderick", Title="Senior SDE" },
                new { FirstName = "Scott", LastName="Hanselman", Title="Principal Program Manager" }
            };
        }

        public IEnumerable Grid2_GetData()
        {
            // Fake long DB call, never sleep in ASP.NET :)
            Thread.Sleep(TimeSpan.FromSeconds(5));

            return new[] {
                new { FirstName = "Scott", LastName="Hunter", Title="Principal Group Program Manager" },
                new { FirstName = "Eilon", LastName="Lipton", Title="Principal Development Manager" }
            };
        }
    }
}