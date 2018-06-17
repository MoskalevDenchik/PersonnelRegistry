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
        private readonly IProvider<WorkStatus> _workStatusProvider;
        private readonly IEntityService<WorkStatus> _workStatusService;

        public WorkStatusController(IProvider<WorkStatus> workStatusProvider, IEntityService<WorkStatus> workStatusService)
        {
            Inspector.ThrowExceptionIfNull(workStatusProvider, workStatusService);
            _workStatusProvider = workStatusProvider;
            _workStatusService = workStatusService;
        }

        public ActionResult Index()
        {
            var list = _workStatusProvider.GetAll();
            return View(list);
        }

        public ActionResult Details(int id = 0)
        {
            var model = _workStatusProvider.GetById(id);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(WorkStatusCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = MapWorkStatusCreateViewModelToWorkStatus(model);
            _workStatusService.Save(user);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            var workStatus = _workStatusProvider.GetById(id);
            var model = MapWorkStatusToWorkStatusEditViewModel(workStatus);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(WorkStatusEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = MapWorkStatusEditViewModelToWorkStatus(model);
            _workStatusService.Save(user);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            _workStatusService.Remove(id);
            return RedirectToAction("Index");
        }

        #region Partial and Json

        [AllowAnonymous]
        [ChildActionOnly]
        public PartialViewResult GetWorkStatusList(int selectedId = 0)
        {
            ViewBag.workStatusId = selectedId;
            var list = _workStatusProvider.GetAll();
            return PartialView("WorkStatusSelect", list);
        }


        #endregion

        #region Mappers

        private WorkStatus MapWorkStatusCreateViewModelToWorkStatus(WorkStatusCreateViewModel model)
        {
            return new WorkStatus
            {
                Status = model.Status
            };
        }

        private WorkStatusEditViewModel MapWorkStatusToWorkStatusEditViewModel(WorkStatus workStatus)
        {
            return new WorkStatusEditViewModel
            {
                Id = workStatus.Id,
                Status = workStatus.Status
            };
        }

        private WorkStatus MapWorkStatusEditViewModelToWorkStatus(WorkStatusEditViewModel model)
        {
            return new WorkStatus
            {
                Id = model.Id,
                Status = model.Status
            };
        }

        #endregion
    }
}