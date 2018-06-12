using DM.PR.WEB.Models.Navigation;
using System.Collections.Generic;
using DM.PR.Business.Providers;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Web.Mvc;
using System;
using DM.PR.WEB.Infrastructure.Attributes;
using System.Linq;

namespace DM.PR.WEB.Controllers
{
    public class NavigationController : Controller
    {
        private readonly IDepartmentProvider _departmentProvider;

        public NavigationController(IDepartmentProvider departmentProvider)
        {
            Inspector.ThrowExceptionIfNull(departmentProvider);
            _departmentProvider = departmentProvider;
        }


        public ActionResult Menu()
        {
            var departments = _departmentProvider.GetAll();
            if (departments == null)
            {
                throw new Exception();
            }

            var model = MapDepartmentToDepartmentViewModel(departments);

            return PartialView(model.Where(x => x.ParentId == null).ToList());
        }

        [AjaxOnly]
        public ActionResult GetDepartmentChildren(int departmentId)
        {
            var departments = _departmentProvider.GetAll();
            if (departments == null)
            {
                throw new Exception();
            }
            var model = MapDepartmentToDepartmentViewModel(departments).Where(x => x.ParentId == departmentId).ToList();

            return PartialView("DepartmentChildren", model);
        }


        #region Helpers

        private IReadOnlyCollection<NavigationMenuViewModel> MapDepartmentToDepartmentViewModel(IEnumerable<Department> departments)
        {
            var departmentsViewModel = new List<NavigationMenuViewModel>();
            foreach (var item in departments)
            {
                departmentsViewModel.Add(new NavigationMenuViewModel()
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