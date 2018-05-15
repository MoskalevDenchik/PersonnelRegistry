using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class ErrorController : Controller
    {   
        public ActionResult ServerError()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}