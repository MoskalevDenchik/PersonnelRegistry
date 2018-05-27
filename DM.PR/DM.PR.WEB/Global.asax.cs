using DM.PR.Common.Logger;
using DM.PR.WEB.App_Start;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DM.PR.WEB
{
    public class MvcApplication : HttpApplication
    {
        //private IRecordLog _log;

        //public MvcApplication(IRecordLog log)
        //{
        //    _log = log;
        //}


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_Error(Object sender, EventArgs e)
        //{

        //    var exc = Server.GetLastError();
        //    if (exc != null)
        //    {
        //        _log.MakeInfo(exc.Message);
        //    }


        //}
    }
}
