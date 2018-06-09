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

        private IKindPhoneProvider _kindPhoneProv;
        private IEmployeeProvider _employeeProvider;
        private IWorkStatusProvider _workStatusProvider;
        private IEmployeeService _employeeService;
        private IDepartmentProvider _departmentProvider;
        private IMaritalStatusProvider _maritalStatusProvider;

        #endregion

        #region Ctors

        public EmployeesController(IEmployeeProvider employeeProvider, IEmployeeService employeeService, IWorkStatusProvider workStatusProvider,
            IDepartmentProvider departmentProvider, IMaritalStatusProvider maritalStatusProvider, IKindPhoneProvider kindPhoneProv)
        {
            Helper.ThrowExceptionIfNull(employeeProvider, employeeService,
                departmentProvider, maritalStatusProvider, kindPhoneProv, workStatusProvider);
            _kindPhoneProv = kindPhoneProv;
            _employeeProvider = employeeProvider;
            _employeeService = employeeService;
            _departmentProvider = departmentProvider;
            _maritalStatusProvider = maritalStatusProvider;
            _workStatusProvider = workStatusProvider;
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeCreateViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            var employee = MapEmployeeCreateViewModelToEmployee(model);
            _employeeService.Create(employee);

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

        #region Partial

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
        public ActionResult GetPageEmployeesBySearchParams(string middledName, string firstName, string lastName, int WorkStatusId, int pageNumber, int pageSize, int fromYear = 0, int toYear = 100)
        {
            var list = _employeeProvider.GetPageBySearchParams(lastName, firstName, middledName, fromYear, toYear, WorkStatusId, pageSize, pageNumber, out int totalCount);
            return PartialView("EmployeeSummary", new PagedData<Employee>(list, totalCount));
        }

        [ChildActionOnly]
        public PartialViewResult GetMaritalStatusList()
        {
            var list = _maritalStatusProvider.GetAll();
            return PartialView("MaritalStatusSelect", list);
        }

        [ChildActionOnly]
        public PartialViewResult GetWorkStatusList()
        {
            var list = _workStatusProvider.GetAll();
            return PartialView("WorkStatusSelect", list);
        }

        [ChildActionOnly]
        public PartialViewResult GetDepartmentList()
        {
            var list = _departmentProvider.GetAll();
            var model = MapDepartmentToDepartmentSelectModel(list);
            return PartialView("DepartmentSelect", model);
        }

        [AjaxOnly]
        public ActionResult AddPhone(int number = 0)
        {
            var list = _kindPhoneProv.GetAll();
            return PartialView("AddPhone", new AddPhoneViewModel { KindList = list, Number = number });
        }

        [AjaxOnly]
        public ActionResult AddEmail(int number = 0)
        {
            return PartialView("AddEmail", number);
        }

        #endregion

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

        private EmployeeDetailsViewModel MapEmployeeToEmployeeDetailsViewModel(Employee empl)
        {
            return new EmployeeDetailsViewModel()
            {
                Id = empl.Id,
                DepartmentName = empl.Department.Name,
                FirstName = empl.FirstName,
                LastName = empl.LastName,
                MiddleName = empl.MiddleName,
                MaritalStatus = empl.MaritalStatus.Status,
                WorkStatus = empl.WorkStatus.Status,
                Address = empl.Address,
                BeginningOfWork = empl.BeginningWork,
                EndOfWork = empl.EndWork,
                ImagePath = empl.ImagePath,
                Phones = empl.Phones,
                Emails = empl.Emails
            };
        }

        private Employee MapEmployeeCreateViewModelToEmployee(EmployeeCreateViewModel model)
        {
            return new Employee()
            {
                Department = new Department { Id = model.DepartmentId },
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Address = model.Address,
                BeginningWork = model.BeginningWork,
                WorkStatus = new WorkStatus { Id = model.WorkStatusId },
                EndWork = model.EndWork,
                ImagePath = model.ImagePath,
                Phones = model.Phones,
                Emails = model.Emails,
                MaritalStatus = new MaritalStatus { Id = model.DepartmentId }
            };
        }

        #endregion
    }
}