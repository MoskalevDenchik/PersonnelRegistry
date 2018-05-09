using DM.PR.Business.Interfaces;
using System.Web.Mvc;
using System.Linq;
using DM.PR.WEB.Models;
using DM.PR.Common.Entities;
using System;
using System.Collections.Generic;

namespace DM.PR.WEB.Controllers
{
    public class NavController : Controller
    {
        private IDepartmentProvider _departmentProvider;

        public NavController(IDepartmentProvider departmentProvider)
        {
            _departmentProvider = departmentProvider;
        }

        public PartialViewResult Menu() => PartialView(_departmentProvider.GetAllAsNavModel());


        #region Helpers

        //    private IEnumerable<NavDepartmentViewModel> MapDepartmentToDepartmentViewModel(IEnumerable<Department> departments)
        //    {
        //        var departmentsViewModel = new List<NavDepartmentViewModel>();
        //        foreach (var item in departments)
        //        {
        //            departmentsViewModel.Add(new NavDepartmentViewModel()
        //            {
        //                Id = item.Id,
        //                Name = item.Name,
        //                Children = FindChidrenById(item.Id)
        //            });
        //        }
        //        return departmentsViewModel;
        //    }

        //    private IEnumerable<NavDepartmentViewModel> FindChidrenById(int? id)
        //    {
        //        IEnumerable<Department> childrenList = _departmentProvider.GetAll().Where(x => x.ParentId == id);

        //        return childrenList != null ? MapDepartmentToDepartmentViewModel(childrenList) : null;

        //    }
    }
    #endregion


}