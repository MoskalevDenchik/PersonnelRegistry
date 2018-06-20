using DM.PR.WEB.Infrastructure.Bindings;
using DM.PR.WEB.DependencyResolution;
using DM.PR.Common.Entities.Account;
using System.Web.Optimization;
using DM.PR.Business.Helpers;
using DM.PR.Common.Entities;
using System.Web.Security;
using DM.PR.Common.Logger;
using DM.PR.WEB.App_Start;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web;
using System;

namespace DM.PR.WEB
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ModelBinders.Binders.Add(typeof(User), new UserBinder());
            ModelBinders.Binders.Add(typeof(Employee), new EmployeeBinder());
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
                    default:
                        errorType = "ServerError";
                        break;
                }
            }


            using (var container = IoC.Initialize())
            {
                var log = container.GetInstance<IRecordLog>();
                log.MakeInfo(exc.Message);
            }

            Response.Redirect($"~/Error/{errorType}");
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            if (cookie != null)
            {
                HttpContext.Current.User = CookiesConverter.ConvertToIPrincipal(cookie);
            }
        }
    }
}