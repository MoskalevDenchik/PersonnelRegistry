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
        private readonly IProvider<WorkStatus> _workStatusProv;
        private readonly IEntityService<WorkStatus> _workStatusServ;

        public WorkStatusController(IProvider<WorkStatus> workStatusProvider, IEntityService<WorkStatus> workStatusService)
        {
            Inspector.ThrowExceptionIfNull(workStatusProvider, workStatusService);
            _workStatusProv = workStatusProvider;
            _workStatusServ = workStatusService;
        }

        public ActionResult Index()
        {
            var list = _workStatusProv.GetAll();
            return list != null ? View(list) : (ActionResult)RedirectToAction("ServerError", "Error");
        }

        public ActionResult Details(int id = 0)
        {
            var model = _workStatusProv.GetById(id);
            return model != null ? View(model) : (ActionResult)RedirectToAction("ServerError", "Error");
        }

        public ActionResult Create() => View();

        public ActionResult Edit(int id = 0)
        {
            var status = _workStatusProv.GetById(id);
            return View(new WorkStatusEditViewModel { Id = status.Id, Status = status.Status });
        }

        public ActionResult Delete(int id = 0)
        {
            _workStatusServ.Remove(id);
            return RedirectToAction("Index");
        }

        [AjaxOnly]
        [HttpPost]
        public JsonResult Save(WorkStatus model) => Json(_workStatusServ.Save(model));
    }
}