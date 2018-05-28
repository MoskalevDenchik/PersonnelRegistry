using DM.PR.Business.Providers;
using DM.PR.Common.Helpers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class KindPhoneController : Controller
    {
        private readonly IKindPhoneProvider _kindPhoneProv;
        public KindPhoneController(IKindPhoneProvider kindPhoneProv)
        {
            Helper.ThrowExceptionIfNull(kindPhoneProv);
            _kindPhoneProv = kindPhoneProv;
        }

        public ActionResult GetAll()
        {
            var kinds = _kindPhoneProv.GetAll();
            return Json(kinds, JsonRequestBehavior.AllowGet);
        }
    }
}