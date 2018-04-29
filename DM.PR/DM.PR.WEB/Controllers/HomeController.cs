using System;
using System.Web.Mvc;
using DM.PR.Business.Interfaces;  
using DM.PR.Common.Logger;

namespace DM.PR.WEB.Controllers
{
    public class HomeController : Controller
    {
        private IDepartmentServices _departmentServices;
        private IRecordLog _recoedLog;

        public HomeController(IDepartmentServices departmentServices, IRecordLog recoedLog)
        {
            _departmentServices = departmentServices;
            _recoedLog = recoedLog;
        }

        public ActionResult Index()
        {
            _recoedLog.MakeInfo("Hello");
            return View(_departmentServices.GetAll());
        }
    }
}