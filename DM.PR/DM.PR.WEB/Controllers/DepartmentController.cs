using DM.PR.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentProvider _departmentProvider;

        public DepartmentController(IDepartmentProvider departmentProvider)
        {
            _departmentProvider = departmentProvider;
        }

        public PartialViewResult Menu()
        {
            return PartialView(_departmentProvider.GetListOfName());
        }


    }
}