using DM.PR.Business.Providers;
using DM.PR.Common.Entities;
using DM.PR.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeProvider _employeeProvider;
        public EmployeeController(EmployeeProvider employeeProvider)
        {
            _employeeProvider = employeeProvider;
        }

        public PartialViewResult List()
        {                                    
            var list = new List<EmployeeListViewModel>();

            foreach (var item in _employeeProvider.GetAll())
            {
                list.Add(MapEmployeeToEmployeeListViewModel(item));
            }
            return PartialView(list);
        }

        #region Mappers
        public EmployeeListViewModel MapEmployeeToEmployeeListViewModel(Employee employee)
        {
            return new EmployeeListViewModel()
            {
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                FirstName = employee.FirstName,
                DepartmentName = employee.Department.Name,
                WorkPhone = employee.Phones
            };
        }
        #endregion

    }
}