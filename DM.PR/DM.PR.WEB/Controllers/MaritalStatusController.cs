using DM.PR.WEB.Infrastructure.Attributes;
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
        #region Private

        private readonly IProvider<MaritalStatus> _maritalStatusProv;
        private readonly IEntityService<MaritalStatus> _maritalStatusServ;

        #endregion

        #region Ctors

        public MaritalStatusController(IProvider<MaritalStatus> maritalStatusProvider, IEntityService<MaritalStatus> maritalStatusServ)
        {
            Inspector.ThrowExceptionIfNull(maritalStatusProvider, maritalStatusServ);
            _maritalStatusProv = maritalStatusProvider;
            _maritalStatusServ = maritalStatusServ;
        }

        #endregion

        [RedirectIfNull(RedirectTo = "~/Error/ServerError")]
        public ActionResult Index()
        {
            return View(_maritalStatusProv.GetAll());
        }

        [RedirectIfNull(RedirectTo = "~/Error/ServerError")]
        public ActionResult Details(int id = 0)
        {
            return View(_maritalStatusProv.GetById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [RedirectIfNull(RedirectTo = "~/Error/ServerError")]
        public ActionResult Edit(int id = 0)
        {
            return View(MapToMaritalStatusEditViewModel(_maritalStatusProv.GetById(id)));
        }

        public ActionResult Delete(int id = 0)
        {
            _maritalStatusServ.Remove(id);
            return RedirectToAction("Index");
        }

        [AjaxOnly]
        [HttpPost]
        public JsonResult Save(MaritalStatus maritalStatus)
        {
            return Json(_maritalStatusServ.Save(maritalStatus));
        }

        #region Mappers

        private MaritalStatusSaveViewModel MapToMaritalStatusEditViewModel(MaritalStatus status)
        {
            return status != null ? new MaritalStatusSaveViewModel { Id = status.Id, Status = status.Status } : null;
        }

        #endregion
    }
}