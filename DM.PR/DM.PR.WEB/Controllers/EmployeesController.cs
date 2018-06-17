using DM.PR.WEB.Infrastructure.Attributes;
using System.Collections.Generic;
using DM.PR.WEB.Models.Employee;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.WEB.Models;
using System.Web.Mvc;
using System.Linq;
using System.IO;

namespace DM.PR.WEB.Controllers
{
    public class EmployeesController : Controller
    {
        #region Private

        private IEmployeeProvider _employeeProvider;
        private IDepartmentProvider _departmentProvider;
        private IEntityService<Employee> _employeeService;

        #endregion

        #region Ctors

        public EmployeesController(IEmployeeProvider employeeProvider, IEntityService<Employee> employeeService, IDepartmentProvider departmentProvider)
        {
            Inspector.ThrowExceptionIfNull(employeeProvider, employeeService, departmentProvider);
            _departmentProvider = departmentProvider;
            _employeeProvider = employeeProvider;
            _employeeService = employeeService;
        }
        #endregion

        [Authorize(Roles = "admin,editor")]
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

        [Authorize(Roles = "admin,editor")]
        public ActionResult Details(int id = 0)
        {
            var employee = _employeeProvider.GetById(id);
            var model = MapEmployeeToEmployeeDetailsViewModel(employee);
            return View(model);
        }

        [Authorize(Roles = "admin")]
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

            var employee = MapEmployeeCreateViewModelToEmployee(model);
            _employeeService.Save(employee);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id = 0)
        {
            var employee = _employeeProvider.GetById(id);
            var model = MapEmployeeToEmployeeCreateViewModel(employee);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var employee = MapEmployeeEditViewModelToEmployee(model);
            _employeeService.Save(employee);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id = 0)
        {
            _employeeService.Remove(id);
            return RedirectToAction("Index");
        }

        #region Partial and Json

        [AjaxOnly]
        public JsonResult AddImage()
        {
            string path = null;
            var data = System.Web.HttpContext.Current.Request.Files["imageBrowes"];
            if (data != null)
            {
                path = $"/Content/Images/{Path.GetFileName(data.FileName)}";
                data.SaveAs(Server.MapPath(path));
            }
            return Json(new { imagePath = path });
        }

        [AjaxOnly]
        public ActionResult GetPageEmployees(int pageSize, int pageNumber)
        {
            var list = _employeeProvider.GetEmployees(pageSize, pageNumber, out int totalCount);
            return Json(new { Data = list, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult GetPageEmployeesByDepartmentId(int departmentId, int pageNumber, int pageSize)
        {
            var list = _employeeProvider.GetEmployees(departmentId, pageSize, pageNumber, out int totalCount);
            ViewBag.totalCount = totalCount;
            return PartialView("EmployeeSummary", list);
        }

        [AjaxOnly]
        public ActionResult GetPageEmployeesBySearchParams(string middledName, string firstName, string lastName, int pageNumber, int pageSize, int WorkStatusId = 0, int fromYear = 0, int toYear = 100)
        {
            var list = _employeeProvider.GetEmployees(lastName, firstName, middledName, fromYear, toYear, WorkStatusId, pageSize, pageNumber, out int totalCount);
            ViewBag.totalCount = totalCount;
            return PartialView("EmployeeSummary", list);
        }


        [ChildActionOnly]
        public PartialViewResult GetDepartmentList(int selectedId = 0)
        {
            ViewBag.departmentId = selectedId;
            var list = _departmentProvider.GetAll();
            var model = MapDepartmentToDepartmentSelectModel(list);
            return PartialView("DepartmentSelect", model);
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
            return departments.Select(d => new DepartmentSelectModel { Id = d.Id, Name = d.Name }).ToList();
        }

        private EmployeeDetailsViewModel MapEmployeeToEmployeeDetailsViewModel(Employee empl)
        {
            return new EmployeeDetailsViewModel()
            {
                Id = empl.Id,
                Emails = empl.Emails,
                Address = empl.Address,
                LastName = empl.LastName,
                EndOfWork = empl.EndWork,
                FirstName = empl.FirstName,
                ImagePath = empl.ImagePath,
                MiddleName = empl.MiddleName,
                WorkStatus = empl.WorkStatus.Status,
                BeginningOfWork = empl.BeginningWork,
                DepartmentName = empl.Department.Name,
                MaritalStatus = empl.MaritalStatus.Status
            };
        }

        private Employee MapEmployeeCreateViewModelToEmployee(EmployeeCreateViewModel model)
        {
            return new Employee()
            {
                Emails = model.Emails,
                Address = model.Address,
                EndWork = model.EndWork,
                LastName = model.LastName,
                ImagePath = model.ImagePath,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                BeginningWork = model.BeginningWork,
                WorkStatus = new WorkStatus { Id = model.WorkStatusId },
                Department = new Department { Id = model.DepartmentId },
                MaritalStatus = new MaritalStatus { Id = model.MaritalStatusId }
            };
        }

        private EmployeeEditViewModel MapEmployeeToEmployeeCreateViewModel(Employee empl)
        {
            return new EmployeeEditViewModel()
            {
                Id = empl.Id,
                Emails = empl.Emails,
                Address = empl.Address,
                EndWork = empl.EndWork,
                LastName = empl.LastName,
                ImagePath = empl.ImagePath,
                FirstName = empl.FirstName,
                MiddleName = empl.MiddleName,
                DepartmentId = empl.Department.Id,
                WorkStatusId = empl.WorkStatus.Id,
                BeginningWork = empl.BeginningWork,
                MaritalStatusId = empl.MaritalStatus.Id
            };
        }

        private Employee MapEmployeeEditViewModelToEmployee(EmployeeEditViewModel model)
        {
            return new Employee()
            {
                Id = model.Id,
                Emails = model.Emails,
                Address = model.Address,
                EndWork = model.EndWork,
                LastName = model.LastName,
                FirstName = model.FirstName,
                ImagePath = model.ImagePath,
                MiddleName = model.MiddleName,
                BeginningWork = model.BeginningWork,
                WorkStatus = new WorkStatus { Id = model.WorkStatusId },
                Department = new Department { Id = model.DepartmentId },
                MaritalStatus = new MaritalStatus { Id = model.MaritalStatusId }
            };
        }

        #endregion
    }
}