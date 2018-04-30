using DM.PR.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class DepartmentsController : Controller
    {
        private IDepartmentProvider _departmentProvider;

        public DepartmentsController(IDepartmentProvider departmentProvider)
        {
            _departmentProvider = departmentProvider;
        }

        public PartialViewResult Menu()
        {
            return PartialView(_departmentProvider.GetListOfName());
        }

        public ActionResult Details(string department)
        {
            if (department!=null)
            {
                return View(_departmentProvider.FindByName(department));
            }
            return View();

        }


    }
}