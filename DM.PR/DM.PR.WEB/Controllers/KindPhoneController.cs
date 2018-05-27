using DM.PR.Business.Providers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class KindPhoneController : Controller
    {
        private IKindPhoneProvider _kindPhoneProv;
        public KindPhoneController(IKindPhoneProvider kindPhoneProv)
        {
            _kindPhoneProv = kindPhoneProv;
        }

        public ActionResult GetAll()
        {
            var kinds = _kindPhoneProv.GetAll();                                         
            return Json(kinds,JsonRequestBehavior.AllowGet);
        }
    }
}