using DM.PR.WEB.Infrastructure.Attributes;
using DM.PR.Common.Entities.Account;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.WEB.Models.User;
using DM.PR.Common.Helpers;
using System.Web.Mvc;
using System.Linq;

namespace DM.PR.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        #region Private

        private readonly IUserProvider _userProvider;
        private readonly IProvider<Role> _roleProvider;
        private readonly IEntityService<User> _userServ;

        #endregion

        #region Ctors
        public UserController(IUserProvider userProv, IEntityService<User> userServ, IProvider<Role> roleProvider)
        {
            Inspector.ThrowExceptionIfNull(userProv, userProv, roleProvider);
            _roleProvider = roleProvider;
            _userProvider = userProv;
            _userServ = userServ;
        }

        #endregion

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

        [HttpGet]
        public ActionResult Create(int employeeId = 0)
        {
            return View(new UserCreateViewModel { EmployeeId = employeeId });
        }

        [AjaxOnly]
        [HttpPost]
        public JsonResult Create(User model)
        {
            var result = _userServ.Save(model);
            return Json(result);
        }

        [HttpGet]
        public ActionResult Edit(int employeeId = 0)
        {
            var user = _userProvider.GetByEmployeeId(employeeId);
            return View(new UserUpdateViewModel
            {
                Id = user.Id,
                EmployeeId = employeeId,
                Login = user.Login,
                Password = user.Password,
                Roles = user.Roles.Select(x => x.Id).ToArray()
            });
        }

        [AjaxOnly]
        [HttpPost]
        public ActionResult Edit(User model)
        {
            var result = _userServ.Save(model);
            return Json(result);
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

    }
}
