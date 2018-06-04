using System;
using System.Web;
using System.Web.Mvc;

namespace DM.PR.WEB.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {                                                   
                throw new HttpException(404, "Not found");                                                      
            }
        }
    }
}