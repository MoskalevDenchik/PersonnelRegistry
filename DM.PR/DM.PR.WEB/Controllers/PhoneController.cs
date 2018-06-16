using DM.PR.WEB.Infrastructure.Attributes;
using System.Collections.Generic; 
using DM.PR.Business.Providers;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class PhoneController : Controller
    {
        private readonly IProvider<KindPhone> _kindPhoneProvider;

        public PhoneController(IProvider<KindPhone> kindPhoneProvider)
        {
            Inspector.ThrowExceptionIfNull(kindPhoneProvider, kindPhoneProvider);
            _kindPhoneProvider = kindPhoneProvider;
        }

        [ChildActionOnly]
        public ActionResult EditPhone(List<Phone> phones)
        {
            ViewBag.phones = phones;
            var list = _kindPhoneProvider.GetAll();
            return PartialView("EditPhone", list);
        }

        [AjaxOnly]
        public ActionResult AddPhone(int number = 0)
        {
            ViewBag.number = number;
            var list = _kindPhoneProvider.GetAll();
            return PartialView("AddPhone", list);
        }
    }
}