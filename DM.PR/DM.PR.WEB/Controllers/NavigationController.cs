using DM.PR.WEB.Infrastructure.Attributes;
using DM.PR.WEB.Models.Navigation;
using System.Collections.Generic;
using DM.PR.Business.Providers;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Web.Mvc;
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

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var departments = _departmentProvider.GetDepartments(0);
            var model = MapDepartmentToDepartmentViewModel(departments);
            return PartialView(model);
        }

        #region Partial and Json

        [AjaxOnly]
        public ActionResult GetDepartmentChildren(int departmentId)
        {
            var departments = _departmentProvider.GetDepartments(departmentId);
            var model = MapDepartmentToDepartmentViewModel(departments);
            return PartialView("DepartmentChildren", model);
        }

        #endregion

        #region Helpers

        private IReadOnlyCollection<NavigationMenuViewModel> MapDepartmentToDepartmentViewModel(IEnumerable<Department> departments)
        {
            return departments.Select(x => new NavigationMenuViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ParentId = x.ParentId
            }).ToList(); ;
        }

        #endregion
    }
}