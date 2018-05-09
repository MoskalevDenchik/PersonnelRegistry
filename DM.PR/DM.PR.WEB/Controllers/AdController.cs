using DM.PR.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class AdController : Controller
    {
        private IAdProvider _adProvider;
        public AdController(IAdProvider adProvider)
        {
            _adProvider = adProvider;
        }
        public PartialViewResult ListOfAd()
        {
           return PartialView(_adProvider.GetContent());
        }
    }
}
