using System;
using System.Web.Mvc;
using DM.PR.Business.Interfaces;
using DM.PR.Common.Logger;

namespace DM.PR.WEB.Controllers
{
    public class HomeController : Controller
    {
        private IRecordLog _recoedLog;

        public HomeController(IRecordLog recoedLog)
        {
            _recoedLog = recoedLog;
        }

        public ActionResult Index()
        {
            return View();
        }
    }

}