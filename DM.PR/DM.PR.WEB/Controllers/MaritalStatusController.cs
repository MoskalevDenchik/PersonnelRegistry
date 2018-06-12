using DM.PR.WEB.Models.MaritalStatus;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class MaritalStatusController : Controller
    {
        private readonly IMaritalStatusProvider _kindPhoneProvider;
        private readonly IMaritalStatusService _kindPhoneServ;

        public MaritalStatusController(IMaritalStatusProvider kindPhoneProvider, IMaritalStatusService kindPhoneServ)
        {
            Inspector.ThrowExceptionIfNull(kindPhoneProvider, kindPhoneProvider);
            _kindPhoneProvider = kindPhoneProvider;
            _kindPhoneServ = kindPhoneServ;
        }

        public ActionResult Index()
        {
            var list = _kindPhoneProvider.GetAll();
            return View(list);
        }

        public ActionResult Details(int id = 0)
        {
            var user = _kindPhoneProvider.GetById(id);
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MaritalStatusCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = MapUserCreateViewModelToUser(model);
            _kindPhoneServ.Create(user);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            var user = _kindPhoneProvider.GetById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(MaritalStatusEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = MapUserEditViewModelToUser(model);
            _kindPhoneServ.Edit(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            _kindPhoneServ.Delete(id);
            return RedirectToAction("Index");
        }

        #region Mappers

        private MaritalStatus MapUserCreateViewModelToUser(MaritalStatusCreateViewModel model)
        {
            return new MaritalStatus
            {
                Status = model.Status
            };
        }

        private MaritalStatus MapUserEditViewModelToUser(MaritalStatusEditViewModel model)
        {
            return new MaritalStatus
            {
                Id = model.Id,
                Status = model.Status
            };
        }

        #endregion
    }
}