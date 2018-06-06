using DM.PR.Common.Entities.Account;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.WEB.Models.User;        
using DM.PR.Common.Helpers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserProvider _userProvider;
        private readonly IUserService _userServ;

        public UserController(IUserProvider userProv, IUserService userServ)
        {
            Helper.ThrowExceptionIfNull(userProv, userProv);
            _userProvider = userProv;
            _userServ = userServ;
        }

        public ActionResult Index()
        {
            var list = _userProvider.GetAll();
            return View(list);
        }

        public ActionResult Details(int id = 0)
        {
            var user = _userProvider.GetById(id);
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = MapUserCreateViewModelToUser(model);
            _userServ.Create(user);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            var user = _userProvider.GetById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = MapUserEditViewModelToUser(model);
            _userServ.Edit(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            _userServ.Delete(id);
            return RedirectToAction("Index");
        }

        #region Mappers

        private User MapUserCreateViewModelToUser(UserCreateViewModel model)
        {
            return new User
            {
                Login = model.Login,
                Password = model.Password,
                Email = model.Email,
                Roles = model.Roles
            };
        }

        private User MapUserEditViewModelToUser(UserEditViewModel model)
        {
            return new User
            {
                Id = model.Id,
                Login = model.Login,
                Password = model.Password,
                Email = model.Email,
                Roles = model.Roles
            };
        }

        #endregion
    }
}
