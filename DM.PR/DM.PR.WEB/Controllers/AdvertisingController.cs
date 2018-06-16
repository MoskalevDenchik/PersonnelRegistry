using DM.PR.Business.Providers;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class AdvertisingController : Controller
    {
        private readonly IProvider<BillBoard> _billBoardProv;

        public AdvertisingController(IProvider<BillBoard> billBoardProv)
        {
            Inspector.ThrowExceptionIfNull(billBoardProv);
            _billBoardProv = billBoardProv;
        }

        [ChildActionOnly]
        public PartialViewResult ListOfAd()
        {
            var content = _billBoardProv.GetAll();
            return content == null ? PartialView("NoAd") : PartialView(content);
        }
    }
}