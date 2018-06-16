using DM.PR.WEB.Models.MaritalStatus;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    [Authorize(Roles = "admin,editor")]
    public class MaritalStatusController : Controller
    {
        private readonly IProvider<MaritalStatus> _maritalStatusProvider;
        private readonly IMaritalStatusService _kindPhoneServ;

        public MaritalStatusController(IProvider<MaritalStatus> maritalStatusProvider, IMaritalStatusService maritalStatusServ)
        {
            Inspector.ThrowExceptionIfNull(maritalStatusProvider, maritalStatusServ);
            _maritalStatusProvider = maritalStatusProvider;
            _kindPhoneServ = maritalStatusServ;
        }

        public ActionResult Index()
        {
            var list = _maritalStatusProvider.GetAll();
            return View(list);
        }

        public ActionResult Details(int id = 0)
        {
            var user = _maritalStatusProvider.GetById(id);
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

            var user = MapMaritalStatusCreateViewModelToMaritalStatus(model);
            _kindPhoneServ.Create(user);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            var maritalStatus = _maritalStatusProvider.GetById(id);
            var model = MapMaritalStatusToMaritalStatusEditViewModel(maritalStatus);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MaritalStatusEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = MapMaritalStatusEditViewModelToMaritalStatus(model);
            _kindPhoneServ.Edit(user);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            _kindPhoneServ.Delete(id);
            return RedirectToAction("Index");
        }

        #region Partial and Json

        [ChildActionOnly]
        public PartialViewResult GetMaritalStatusList(int selectedId = 0)
        {
            ViewBag.maritalStatusId = selectedId;
            var list = _maritalStatusProvider.GetAll();
            return PartialView("MaritalStatusSelect", list);
        }

        #endregion


        #region Mappers

        private MaritalStatus MapMaritalStatusCreateViewModelToMaritalStatus(MaritalStatusCreateViewModel model)
        {
            return new MaritalStatus
            {
                Status = model.Status
            };
        }

        private MaritalStatus MapMaritalStatusEditViewModelToMaritalStatus(MaritalStatusEditViewModel model)
        {
            return new MaritalStatus
            {
                Id = model.Id,
                Status = model.Status
            };
        }

        private MaritalStatusEditViewModel MapMaritalStatusToMaritalStatusEditViewModel(MaritalStatus maritalStatus)
        {
            return new MaritalStatusEditViewModel
            {
                Id = maritalStatus.Id,
                Status = maritalStatus.Status
            };
        }

        #endregion
    }
}