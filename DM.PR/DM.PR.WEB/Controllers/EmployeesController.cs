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

        [Authorize(Roles = "admin")]
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
            var empl = _employeeProvider.GetById(id);
            return View(MapEmployeeToEmployeeDetailsViewModel(empl));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id = 0)
        {
            var empl = _employeeProvider.GetById(id);
            return View(MapEmployeeToEmployeeEditViewModel(empl));
        }

        [HttpPost]
        [AjaxOnly]
        [Authorize(Roles = "admin")]
        public JsonResult Save(Employee employee)
        {
            var result = _employeeService.Save(employee);
            return Json(result);
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
        public ActionResult GetEmployeesByDepartmentId(int departmentId, int pageNumber, int pageSize)
        {
            var list = _employeeProvider.GetEmployees(departmentId, pageSize, pageNumber, out int totalCount);
            ViewBag.totalCount = totalCount;
            var model = MapEmployeesToEmployeesSummaryViewModel(list);
            return PartialView("EmployeeSummary", model);
        }

        [AjaxOnly]
        public ActionResult GetEmployees(string middleName, string firstName, string lastName, int pageNumber, int pageSize, int WorkStatusId = 0, int fromYear = 0, int toYear = 100)
        {
            var list = _employeeProvider.GetEmployees(lastName, firstName, middleName, fromYear, toYear, WorkStatusId, pageSize, pageNumber, out int totalCount);
            ViewBag.totalCount = totalCount;
            var model = MapEmployeesToEmployeesSummaryViewModel(list);
            return PartialView("EmployeeSummary", model);
        }

        [ChildActionOnly]
        public PartialViewResult GetDepartmentList(int selectedId = 0)
        {
            ViewBag.departmentId = selectedId;
            var list = _departmentProvider.GetAll();
            var model = MapDepartmentToDepartmentSelectModel(list);
            return PartialView("DepartmentSelect", model);
        }

        #endregion

        #region Mappers

        private IReadOnlyCollection<DepartmentSelectModel> MapDepartmentToDepartmentSelectModel(IReadOnlyCollection<Department> departments)
        {
            return departments.Select(d => new DepartmentSelectModel { Id = d.Id, Name = d.Name }).ToList();
        }

        private IReadOnlyCollection<EmployeeSummaryViewModel> MapEmployeesToEmployeesSummaryViewModel(IReadOnlyCollection<Employee> list)
        {
            return list.Select(empl => new EmployeeSummaryViewModel
            {
                Id = empl.Id,
                HasRole = empl.HasRole,
                LastName = empl.LastName,
                WorkPhone = empl.WorkPhone,
                FirstName = empl.FirstName,
                ImagePath = empl.ImagePath,
                MiddleName = empl.MiddleName,
                DepartmentName = empl.Department.Name

            }).ToList();
        }

        private EmployeeDetailsViewModel MapEmployeeToEmployeeDetailsViewModel(Employee empl) => new EmployeeDetailsViewModel
        {
            Id = empl.Id,
            Emails = empl.Emails,
            Address = empl.Address,
            LastName = empl.LastName,
            EndOfWork = empl.EndWork,
            HomePhone = empl.HomePhone,
            MobilePhone = empl.MobilePhone,
            WorkPhone = empl.WorkPhone,
            FirstName = empl.FirstName,
            ImagePath = empl.ImagePath,
            MiddleName = empl.MiddleName,
            WorkStatus = empl.WorkStatus.Status,
            BeginningOfWork = empl.BeginningWork,
            DepartmentName = empl.Department.Name,
            MaritalStatus = empl.MaritalStatus.Status
        };

        private EmployeeEditViewModel MapEmployeeToEmployeeEditViewModel(Employee empl) => new EmployeeEditViewModel
        {
            Id = empl.Id,
            Emails = empl.Emails,
            Address = empl.Address,
            EndWork = empl.EndWork,
            HomePhone = empl.HomePhone,
            MobilePhone = empl.MobilePhone,
            WorkPhone = empl.WorkPhone,
            LastName = empl.LastName,
            ImagePath = empl.ImagePath,
            FirstName = empl.FirstName,
            MiddleName = empl.MiddleName,
            DepartmentId = empl.Department.Id,
            WorkStatusId = empl.WorkStatus.Id,
            BeginningWork = empl.BeginningWork,
            MaritalStatusId = empl.MaritalStatus.Id
        };

        #endregion
    }
}