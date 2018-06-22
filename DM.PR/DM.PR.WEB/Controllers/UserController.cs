using DM.PR.WEB.Infrastructure.Attributes;
using DM.PR.Common.Entities.Account;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.WEB.Models.User;
using DM.PR.Common.Helpers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        #region Private

        private readonly IUserProvider _userProv;
        private readonly IProvider<Role> _roleProv;
        private readonly IEntityService<User> _userServ;

        #endregion

        #region Ctors
        public UserController(IUserProvider userProv, IEntityService<User> userServ, IProvider<Role> roleProvider)
        {
            Inspector.ThrowExceptionIfNull(userProv, userProv, roleProvider);
            _roleProv = roleProvider;
            _userProv = userProv;
            _userServ = userServ;
        }

        #endregion

        public ActionResult Index()
        {
            var list = _userProv.GetAll();
            return View(list);
        }

        public ActionResult Details(int id = 0)
        {
            var user = _userProv.GetById(id);
            return View(user);
        }

        public ActionResult Create(int employeeId = 0)
        {
            var rolesList = _roleProv.GetAll();
            return View(new UserSaveViewModel { EmployeeId = employeeId, RolesList = rolesList });
        }

        public ActionResult Edit(int employeeId = 0)
        {
            var user = _userProv.GetByEmployeeId(employeeId);
            var rolesList = _roleProv.GetAll();
            return View(new UserSaveViewModel
            {
                Id = user.Id,
                EmployeeId = employeeId,
                Login = user.Login,
                Password = user.Password,
                RolesList = rolesList,
                Roles =  user.Roles.ToArray()
            });
        }

        public ActionResult Delete(int id = 0)
        {
            _userServ.Remove(id);
            return RedirectToAction("Index");
        }

        [AjaxOnly]
        [HttpPost]
        public JsonResult Save(User model) => Json(_userServ.Save(model));
    }
}
