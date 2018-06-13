using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    [Authorize(Roles = "admin,editor")]
    public class ManageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}