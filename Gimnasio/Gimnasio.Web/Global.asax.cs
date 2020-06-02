using Gimnasio.Web.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Gimnasio.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            this.CheckRoles();
            Utilities.CheckManager();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void CheckRoles()
        {
            Utilities.CheckRoles("Admin");
        }
    }
}
