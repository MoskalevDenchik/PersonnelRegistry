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
        private readonly IUserProvider _userProvider;
        private readonly IProvider<Role> _roleProvider;
        private readonly IUserService _userServ;

        public UserController(IUserProvider userProv, IUserService userServ, IProvider<Role> roleProvider)
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
            var model = new UserCreateViewModel { EmployeeId = employeeId };
            return View(model);
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

        public ActionResult Edit(int employeeId = 0)
        {
            var user = _userProvider.GetByEmployeeId(employeeId);
            var model = MapUserToUserEditViewModel(user);
            return View(model);
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

        public ActionResult Delete(int id = 0)
        {
            _userServ.Delete(id);
            return RedirectToAction("Index");
        }

        #region Partial

        public ActionResult AddRole(int selectedId = 0, int number = 0)
        {
            ViewBag.number = number;
            ViewBag.selectedId = selectedId;
            var roles = _roleProvider.GetAll();
            return PartialView("SelectRole", roles);
        }

        #endregion

        #region Mappers

        private User MapUserCreateViewModelToUser(UserCreateViewModel model)
        {
            return new User
            {
                EmployeeId = model.EmployeeId,
                Login = model.Login,
                Password = model.Password,
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
                Roles = model.Roles
            };
        }

        private UserEditViewModel MapUserToUserEditViewModel(User model)
        {
            return new UserEditViewModel
            {
                Id = model.Id,
                Login = model.Login,
                Password = model.Password,
                Roles = model.Roles.ToList()
            };
        }

        #endregion
    }
}
