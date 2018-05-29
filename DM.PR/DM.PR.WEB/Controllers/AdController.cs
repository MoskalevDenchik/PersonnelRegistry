using DM.PR.Business.Providers;
using DM.PR.Common.Helpers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class AdController : Controller
    {
        private readonly IAdProvider _adProvider;

        public AdController(IAdProvider adProvider)
        {
            Helper.ThrowExceptionIfNull(adProvider);
            _adProvider = adProvider;
        }

        public PartialViewResult ListOfAd()
        {
            var content = _adProvider.GetContent();

            return content.Equals(null) ? PartialView("NoAd") : PartialView(content);
        }
    }
}