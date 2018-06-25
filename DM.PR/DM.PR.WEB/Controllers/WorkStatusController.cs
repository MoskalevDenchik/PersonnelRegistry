using DM.PR.WEB.Infrastructure.Attributes;
using DM.PR.WEB.Models.WorkStatus;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    [Authorize(Roles = "admin,editor")]
    public class WorkStatusController : Controller
    {
        #region Private

        private readonly IProvider<WorkStatus> _workStatusProv;
        private readonly IEntityService<WorkStatus> _workStatusServ;

        #endregion

        #region Ctors

        public WorkStatusController(IProvider<WorkStatus> workStatusProvider, IEntityService<WorkStatus> workStatusService)
        {
            Inspector.ThrowExceptionIfNull(workStatusProvider, workStatusService);
            _workStatusProv = workStatusProvider;
            _workStatusServ = workStatusService;
        }

        #endregion

        [RedirectIfNull(RedirectTo = "~/Error/ServerError")]
        public ActionResult Index()
        {
            return View(_workStatusProv.GetAll());
        }

        [RedirectIfNull(RedirectTo = "~/Error/ServerError")]
        public ActionResult Details(int id = 0)
        {
            return View(_workStatusProv.GetById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [RedirectIfNull(RedirectTo = "~/Error/ServerError")]
        public ActionResult Edit(int id = 0)
        {
            return View(MapToWorkStatusEditViewModel(_workStatusProv.GetById(id)));
        }

        public ActionResult Delete(int id = 0)
        {
            _workStatusServ.Remove(id);
            return RedirectToAction("Index");
        }

        [AjaxOnly]
        [HttpPost]
        public JsonResult Save(WorkStatus model)
        {
            return Json(_workStatusServ.Save(model));
        }

        #region Mappers

        private WorkStatusSaveViewModel MapToWorkStatusEditViewModel(WorkStatus status)
        {
            return status != null ? new WorkStatusSaveViewModel { Id = status.Id, Status = status.Status } : null;
        }

        #endregion

    }
}