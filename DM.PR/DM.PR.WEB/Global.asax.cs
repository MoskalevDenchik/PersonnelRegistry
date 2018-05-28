﻿using DM.PR.Common.Logger;
using DM.PR.WEB.App_Start;
using DM.PR.WEB.DependencyResolution;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DM.PR.WEB
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            Server.ClearError();
            Response.Clear();

            string errorType = "ServerError";

            if (exc is HttpException HttpExc)
            {
                switch (HttpExc.GetHttpCode())
                {
                    case 404:
                        errorType = "NotFound";
                        break;
                    case 500:
                        errorType = "ServerError";
                        break;
                    default:
                        errorType = "ServerError";
                        break;
                }
            }

            var container = IoC.Initialize();
            var log = container.GetInstance<IRecordLog>();
            log.MakeInfo(exc.Message);

            Response.Redirect($"~/Error/{errorType}");
        }
    }


}