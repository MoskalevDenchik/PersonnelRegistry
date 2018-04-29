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
        private IDepartmentServices _departmentServices;

        public DepartmentController(IDepartmentServices departmentServices)
        {
            _departmentServices = departmentServices;
        }

        public PartialViewResult Menu()
        {
            return PartialView(_departmentServices.GetListOfName());
        }


    }
}