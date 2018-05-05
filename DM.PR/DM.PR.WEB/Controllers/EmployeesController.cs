using DM.PR.Business.Interfaces;
using DM.PR.Common.Entities;
using DM.PR.Common.Enums;
using DM.PR.WEB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeeProvider _employeeProvider;
        private IEmployeeService _employeeService;
        //---------------------------------------Ctor--------------------------------------------------------------
        public EmployeesController(IEmployeeProvider employeeProvider, IEmployeeService employeeService)
        {
            _employeeProvider = employeeProvider;
            _employeeService = employeeService;
        }
        //--------------------------------------Index--------------------------------------------------------------
        public ActionResult Index()
        {
            return View();
        }

        //--------------------------------------List---------------------------------------------------------------
        public PartialViewResult List(string department)
        {
            var list = new List<EmployeeListViewModel>();
            var list2 = department == null ? _employeeProvider.GetAll()
                : _employeeProvider.FindAllByDepartmentName(department);

            foreach (var item in list2)
            {
                list.Add(MapEmployeeToEmployeeListViewModel(item));
            }

            return PartialView(list);
        }

        //--------------------------------------Create-------------------------------------------------------------
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.Create(employee);
            }
            return View();
        }

        //--------------------------------------Details------------------------------------------------------------
        public ActionResult Details(int? id)
        {
            return View(_employeeProvider.FindById(id));
        }

        //---------------------------------------Edit--------------------------------------------------------------
        public ActionResult Edit(int? id)
        {
            return View(_employeeProvider.FindById(id));
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.Edit(employee);
            }
            return View();
        }

        //--------------------------------------Delete-------------------------------------------------------------
        public ActionResult Delete(int? id)
        {
            _employeeService.Delete(id);
            return RedirectToAction("Index");
        }

        #region Mappers
        public EmployeeListViewModel MapEmployeeToEmployeeListViewModel(Employee employee)
        {
            return new EmployeeListViewModel()
            {
                Id = employee.Id,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                FirstName = employee.FirstName,
                DepartmentName = employee.Department.Name,
                WorkPhone = employee.Phones.FirstOrDefault(p => p.Kind == KindOfPhone.WORK)
            };
        }


        #endregion

    }
}