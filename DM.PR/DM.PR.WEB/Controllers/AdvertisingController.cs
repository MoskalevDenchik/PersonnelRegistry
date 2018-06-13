using DM.PR.Business.Providers;
using DM.PR.Common.Helpers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class AdvertisingController : Controller
    {
        private readonly IBillBoardProvider billBoardProv;

        public AdvertisingController(IBillBoardProvider adProvider)
        {
            Inspector.ThrowExceptionIfNull(adProvider);
            billBoardProv = adProvider;
        }

        [ChildActionOnly]
        public PartialViewResult ListOfAd()
        {
            var content = billBoardProv.GetAll();
            return content == null ? PartialView("NoAd") : PartialView(content);
        }
    }
}