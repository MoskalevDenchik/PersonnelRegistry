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
        private readonly IProvider<MaritalStatus> _maritalStatusProv;
        private readonly IEntityService<MaritalStatus> _maritalStatusServ;

        public MaritalStatusController(IProvider<MaritalStatus> maritalStatusProvider, IEntityService<MaritalStatus> maritalStatusServ)
        {
            Inspector.ThrowExceptionIfNull(maritalStatusProvider, maritalStatusServ);
            _maritalStatusProv = maritalStatusProvider;
            _maritalStatusServ = maritalStatusServ;
        }

        public ActionResult Index()
        {
            var list = _maritalStatusProv.GetAll();
            return View(list);
        }

        public ActionResult Details(int id = 0)
        {
            var user = _maritalStatusProv.GetById(id);
            return View(user);
        }

        public ActionResult Create() => View();

        public ActionResult Edit(int id = 0)
        {
            var status = _maritalStatusProv.GetById(id);
            return View(new MaritalStatusEditViewModel { Id = status.Id, Status = status.Status });
        }

        public ActionResult Delete(int id = 0)
        {
            _maritalStatusServ.Remove(id);
            return RedirectToAction("Index");
        }

        [AjaxOnly] [HttpPost]
        public JsonResult Save(MaritalStatus maritalStatus) => Json(_maritalStatusServ.Save(maritalStatus));
    }
}