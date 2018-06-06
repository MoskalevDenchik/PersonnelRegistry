using DM.PR.WEB.Models.KindPhone;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class KindPhoneController : Controller
    {
        private readonly IKindPhoneProvider _kindPhoneProvider;
        private readonly IKindPhoneService _kindPhoneServ;

        public KindPhoneController(IKindPhoneProvider kindPhoneProvider, IKindPhoneService kindPhoneServ)
        {
            Helper.ThrowExceptionIfNull(kindPhoneProvider, kindPhoneProvider);
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
        public ActionResult Create(KindPhoneCreateViewModel model)
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
        public ActionResult Edit(KindPhoneEditViewModel model)
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

        private KindPhone MapUserCreateViewModelToUser(KindPhoneCreateViewModel model)
        {
            return new KindPhone
            {
                Kind = model.Kind
            };
        }

        private KindPhone MapUserEditViewModelToUser(KindPhoneEditViewModel model)
        {
            return new KindPhone
            {
                Id = model.Id,
                Kind = model.Kind
            };
        }

        #endregion
    }
}