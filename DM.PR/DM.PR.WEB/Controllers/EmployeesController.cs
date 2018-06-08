using DM.PR.WEB.Infrastructure.Attributes;
using System.Collections.Generic;
using DM.PR.WEB.Models.Employee;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.WEB.Models;
using System.Web.Mvc;

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

        public ActionResult Navigation()
        {
            return View("DepartmentNavigation");
        }

        public ActionResult Details(int id = 0)
        {
            var employee = _employeeProvider.GetById(id);
            return View(MapEmployeeToEmployeeDetailsViewModel(employee));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _employeeService.Create(MapEmployeeCreateViewModelToEmployee(model));
            return RedirectToAction("Index");
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
            _employeeService.Delete(id);
            return RedirectToAction("Index");
        }

        [AjaxOnly]
        public ActionResult GetPageEmployees(int pageSize, int pageNumber)
        {
            var list = _employeeProvider.GetPage(pageSize, pageNumber, out int totalCount);
            return Json(new { Data = list, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult GetPageEmployeesByDepartmentId(int departmentId, int pageNumber, int pageSize)
        {
            var list = _employeeProvider.GetPageByDepartmentId(departmentId, pageSize, pageNumber, out int totalCount);
            return PartialView("EmployeeSummary", new PagedData<Employee>(list, totalCount));
        }

        [AjaxOnly]
        public ActionResult GetPageEmployeesBySearchParams(string middledName, string firstName, string lastName, bool IsWorking, int pageNumber, int pageSize, int fromYear = 0, int toYear = 100)
        {
            var list = _employeeProvider.GetPageBySearchParams(lastName, firstName, middledName, fromYear, toYear, IsWorking, pageSize, pageNumber, out int totalCount);
            return PartialView("EmployeeSummary", new PagedData<Employee>(list, totalCount));
        }


        [ChildActionOnly]
        public PartialViewResult AddEmail(int emails)
        {
            return PartialView(emails);
        }

        [ChildActionOnly]
        public PartialViewResult AddPhone(int phones)
        {
            return PartialView(phones);
        }

        [ChildActionOnly]
        public PartialViewResult SelectMaritalStatus()
        {
            var list = _maritalStatusProvider.GetAll();
            return PartialView(list);
        }

        [ChildActionOnly]
        public PartialViewResult SelectList()
        {
            var list = _departmentProvider.GetAll();
            return PartialView(MapDepartmentToDepartmentSelectModel(list));
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