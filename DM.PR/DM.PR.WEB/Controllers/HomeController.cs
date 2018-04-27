using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DM.PR.Business.Interfaces;

namespace DM.PR.WEB.Controllers
{
    public class HomeController : Controller
    {
        private IDepartmentServices _departmentServices;

        public HomeController(IDepartmentServices departmentServices)
        {
            _departmentServices = departmentServices;
        }

        public ActionResult Index()
        {           
            return View(_departmentServices.GetAll());
        }
    }
}