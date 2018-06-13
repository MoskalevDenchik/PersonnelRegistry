using DM.PR.WEB.Models.WorkStatus;  
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class WorkStatusController : Controller
    {
        private readonly IWorkStatusProvider _workStatusProvider;
        private readonly IWorkStatusService _workStatusService;

        public WorkStatusController(IWorkStatusProvider workStatusProvider, IWorkStatusService workStatusService)
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
            var user = _workStatusProvider.GetById(id);
            return View(user);
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

            var user = MapUserCreateViewModelToUser(model);
            _workStatusService.Create(user);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            var user = _workStatusProvider.GetById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(WorkStatusEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = MapUserEditViewModelToUser(model);
            _workStatusService.Edit(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            _workStatusService.Delete(id);
            return RedirectToAction("Index");
        }
        
        #region Partial and Json


        [ChildActionOnly]
        public PartialViewResult GetWorkStatusList(int selectedId = 0)
        {
            ViewBag.workStatusId = selectedId;
            var list = _workStatusProvider.GetAll();
            return PartialView("WorkStatusSelect", list);
        }


        #endregion

        #region Mappers

        private WorkStatus MapUserCreateViewModelToUser(WorkStatusCreateViewModel model)
        {
            return new WorkStatus
            {
                Status = model.Status
            };
        }

        private WorkStatus MapUserEditViewModelToUser(WorkStatusEditViewModel model)
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