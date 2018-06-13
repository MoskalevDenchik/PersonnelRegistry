using DM.PR.Common.Entities.Account;
using DM.PR.WEB.Models.Account;
using DM.PR.Business.Services;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class AccountController : Controller
    {
        ILoginServices _loginServices;

        public AccountController(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {                                          
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            SignInStatus result = _loginServices.SignIn(model.Login, model.Password);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                    return RedirectToLocal(returnUrl);
                case SignInStatus.InvalidPssword:
                    ModelState.AddModelError("", "Вы ввели неверный пароль");
                    return View(model);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Пользователя с таким именем не существует");
                    return View(model);
            }
        }

        public ActionResult LoginOut(string returnUrl)
        {
            _loginServices.SingOut();
            return RedirectToAction("Index", "Home");
        }


        #region Helpers

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}