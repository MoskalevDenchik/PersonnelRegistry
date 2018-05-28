using System.Web.Mvc;
using DM.PR.Common.Entities;
using System.Collections.Generic;
using DM.PR.WEB.Models;
using DM.PR.Business.Providers;
using DM.PR.Common.Helpers;
using System;

namespace DM.PR.WEB.Controllers
{
    public class NavController : Controller
    {
        private readonly IDepartmentProvider _departmentProvider;

        public NavController(IDepartmentProvider departmentProvider)
        {
            Helper.ThrowExceptionIfNull(departmentProvider);
            _departmentProvider = departmentProvider;
        }

        public ActionResult Menu()
        {
            var departments = _departmentProvider.GetAll();
            if (departments.Equals(null))
            {
                throw new Exception();
            }

            return PartialView(MapDepartmentToDepartmentViewModel(departments));
        }

        #region Helpers

        private IReadOnlyCollection<DepartmentNavViewModel> MapDepartmentToDepartmentViewModel(IEnumerable<Department> departments)
        {
            var departmentsViewModel = new List<DepartmentNavViewModel>();
            foreach (var item in departments)
            {
                departmentsViewModel.Add(new DepartmentNavViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ParentId = item.ParentId
                });
            }
            return departmentsViewModel;
        }

        #endregion
    }
}