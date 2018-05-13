using DM.PR.Business.Providers;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class AdController : Controller
    {
        #region Private

        private IAdProvider _adProvider;

        #endregion

        #region Ctor

        public AdController(IAdProvider adProvider)
        {
            _adProvider = adProvider;
        }

        #endregion

        #region ListOfAd

        public PartialViewResult ListOfAd()
        {
            return PartialView(_adProvider.GetContent());
        }
      
        #endregion
    }
}
