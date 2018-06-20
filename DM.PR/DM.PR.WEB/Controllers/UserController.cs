using DM.PR.Common.Entities.Account;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.WEB.Models.User;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Web.Mvc;
using DM.PR.WEB.Infrastructure.Attributes;

namespace DM.PR.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly IUserProvider _userProvider;
        private readonly IProvider<Role> _roleProvider;
        private readonly IEntityService<User> _userServ;

        public UserController(IUserProvider userProv, IEntityService<User> userServ, IProvider<Role> roleProvider)
        {
            Inspector.ThrowExceptionIfNull(userProv, userProv, roleProvider);
            _roleProvider = roleProvider;
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

        public ActionResult Create(int employeeId = 0)
        {
            return View(new UserCreateViewModel { EmployeeId = employeeId });
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult Create(User model)
        {
            var result = _userServ.Save(model);
            return Json(result);
        }

        public ActionResult Edit(int employeeId = 0)
        {
            ViewBag.title = "Редактируйте пользователя";
            var user = _userProvider.GetByEmployeeId(employeeId);
            var model = MapUserToUserSaveViewModel(user);
            return View("Save", model);
        }

        [HttpPost]
        public ActionResult Edit(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", model);
            }

            var user = MapUserSaveViewModelToUser(model);
            _userServ.Save(user);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            _userServ.Remove(id);
            return RedirectToAction("Index");
        }

        #region Partial

        [ChildActionOnly]
        public ActionResult GetRole()
        {
            var list = _roleProvider.GetAll();
            return PartialView(new GetRolePartialModel { Roles = list });
        }

        #endregion

        #region Mappers

        private User MapUserSaveViewModelToUser(UserCreateViewModel model) => new User
        {
            Id = model.Id,
            Login = model.Login,
            Password = model.Password,
        };

        private UserCreateViewModel MapUserToUserSaveViewModel(User model) => new UserCreateViewModel
        {
            Id = model.Id,
            Login = model.Login,
            Password = model.Password,
        };

        #endregion
    }
}
