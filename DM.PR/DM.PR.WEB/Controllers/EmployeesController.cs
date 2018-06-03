using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.WEB.Models;
using DM.PR.WEB.Models.Employee;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System;

namespace DM.PR.WEB.Controllers
{
    public class EmployeesController : Controller
    {
        #region Private

        private IEmployeeProvider _employeeProvider;
        private IEmployeeService _employeeService;
        private IDepartmentProvider _departmentProvider;
        private IMaritalStatusProvider _maritalStatusProvider;

        #endregion

        #region Ctors

        public EmployeesController(IEmployeeProvider employeeProvider, IEmployeeService employeeService,
            IDepartmentProvider departmentProvider, IMaritalStatusProvider maritalStatusProvider)
        {
            Helper.ThrowExceptionIfNull(employeeProvider, employeeService, departmentProvider, maritalStatusProvider);
            _employeeProvider = employeeProvider;
            _employeeService = employeeService;
            _departmentProvider = departmentProvider;
            _maritalStatusProvider = maritalStatusProvider;
        }
        #endregion

        public ActionResult Index()
        {                                                
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }


        public JsonResult GetListBy(string MiddledName, string FirstName, string LastName, DateTime? BeginningWork, bool IsWorking)
        {
            var list = _employeeProvider.FindBy(MiddledName, FirstName, LastName, BeginningWork, IsWorking);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var employee = new EmployeeCreateViewModel
            {
                Emails = new List<Email>(),
                Phones = new List<Phone>()
            };
            return View(employee);
        }

        [HttpPost]
        public ActionResult Create(EmployeeCreateViewModel employee)
        {
            _employeeService.Create(MapEmployeeCreateViewModelToEmployee(employee));
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            var employee = _employeeProvider.GetById(id);
            return View(MapEmployeeToEmployeeDetailsViewModel(employee));
        }

        public ActionResult Edit(int id = 0)
        {
            return View(_employeeProvider.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.Edit(employee);
            }
            return View(employee);
        }

        public ActionResult Delete(int id = 0)
        {
            _employeeService.Delete((int)id);
            return RedirectToAction("Index");
        }

        public PartialViewResult GetListByDepartmentId(int id = 0, int pageSize = 5, int page = 1)
        {
            var model = _employeeProvider.GetAllByDepartmentId(id);
            if (model != null)
            {
                return PartialView(model);
            }
            else
            {
                return PartialView("NoEmployees");
            }
        }

        public ActionResult GetAll(int pageSize, int pageNumber)
        {
            var model = _employeeProvider.GetAll(pageSize, pageNumber);
            model.CurentPage = pageNumber;
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult AddEmail(int emails)
        {
            return PartialView(emails);
        }

        public PartialViewResult AddPhone(int phones)
        {
            return PartialView(phones);
        }

        [ChildActionOnly]
        public PartialViewResult SelectMaritalStatus()
        {
            var list = _maritalStatusProvider.GetAll();
            if (list != null)
            {
                return PartialView(list);
            }
            else
            {
                return null;
            }
        }

        [ChildActionOnly]
        public PartialViewResult SelectList()
        {
            var list = _departmentProvider.GetAll();
            if (list != null)
            {
                return PartialView(MapDepartmentToDepartmentSelectModel(list));
            }
            else return null;
        }

        #region Mappers

        private IReadOnlyCollection<DepartmentSelectModel> MapDepartmentToDepartmentSelectModel(IReadOnlyCollection<Department> departments)
        {
            var list = new List<DepartmentSelectModel>();
            foreach (var item in departments)
            {
                list.Add(new DepartmentSelectModel()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return list;
        }

        private EmployeeDetailsViewModel MapEmployeeToEmployeeDetailsViewModel(Employee employee)
        {
            return new EmployeeDetailsViewModel()
            {
                Id = employee.Id,
                DepartmentName = employee.Department.Name,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Address = employee.Address,
                BeginningOfWork = employee.BeginningWork,
                EndOfWork = employee.EndWork,
                ImagePath = employee.ImagePath,
                Phones = employee.Phones,
                Emails = employee.Emails
            };
        }

        private Employee MapEmployeeCreateViewModelToEmployee(EmployeeCreateViewModel employee)
        {
            return new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Address = employee.Address,
                BeginningWork = employee.BeginningWork,
                EndWork = employee.EndWork,
                ImagePath = employee.ImagePath,
                Phones = employee.Phones,
                Emails = employee.Emails
            };
        }

        #endregion
    }
}