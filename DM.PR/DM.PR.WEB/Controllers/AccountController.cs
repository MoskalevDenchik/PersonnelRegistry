using DM.PR.WEB.Infrastructure.Attributes;
using DM.PR.Business.Services;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class AccountController : Controller
    {
        #region Private

        private readonly ILoginServices _loginServices;

        #endregion

        #region Ctors
        public AccountController(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        #endregion

        public ActionResult Login()
        {
            return View();
        }

        [AjaxOnly]
        [HttpPost]
        public JsonResult Login(string Login, string Password)
        {
            return Json(_loginServices.SignIn(Login, Password));
        }

        public ActionResult Logout()
        {
            _loginServices.SingOut();
            return RedirectToAction("Index", "Home");
        }
    }
}