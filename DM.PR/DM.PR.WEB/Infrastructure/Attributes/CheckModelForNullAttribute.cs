using System.Web.Mvc;

namespace DM.PR.WEB.Infrastructure.Attributes
{
    public class RedirectIfNullAttribute : ActionFilterAttribute
    {

        public string RedirectTo { get; set; }

        public RedirectIfNullAttribute() : base()
        {
            RedirectTo = $"~/Error/ServerError";
        }

        public RedirectIfNullAttribute(string redirectTo) : base()
        {
            RedirectTo = redirectTo;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (((ViewResult)filterContext.Result).Model == null)
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.Redirect(RedirectTo);
            }
        }
    }
}