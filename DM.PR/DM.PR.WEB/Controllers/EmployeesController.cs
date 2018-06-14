using DM.PR.WEB.Infrastructure.Attributes;
using System.Collections.Generic;
using DM.PR.WEB.Models.Employee;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.WEB.Models;
using System.Web.Mvc;
using System.IO;

namespace DM.PR.WEB.Controllers
{
    public class EmployeesController : Controller
    {
        #region Private

        private IEmployeeProvider _employeeProvider;
        private IEmployeeService _employeeService;
        private IDepartmentProvider _departmentProvider;

        #endregion

        #region Ctors

        public EmployeesController(IEmployeeProvider employeeProvider, IEmployeeService employeeService, IDepartmentProvider departmentProvider)
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
            _employeeService.Create(employee);

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
            _employeeService.Edit(employee);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id = 0)
        {
            _employeeService.Delete(id);
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
            var list = _employeeProvider.GetPage(pageSize, pageNumber, out int totalCount);
            return Json(new { Data = list, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult GetPageEmployeesByDepartmentId(int departmentId, int pageNumber, int pageSize)
        {
            var list = _employeeProvider.GetPageByDepartmentId(departmentId, pageSize, pageNumber, out int totalCount);
            ViewBag.totalCount = totalCount;
            return PartialView("EmployeeSummary", list);
        }

        [AjaxOnly]
        public ActionResult GetPageEmployeesBySearchParams(string middledName, string firstName, string lastName, int pageNumber, int pageSize, int WorkStatusId = 0, int fromYear = 0, int toYear = 100)
        {
            var list = _employeeProvider.GetPageBySearchParams(lastName, firstName, middledName, fromYear, toYear, WorkStatusId, pageSize, pageNumber, out int totalCount);
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
                MaritalStatus = new MaritalStatus { Id = model.MaritalStatusId }
            };
        }

        private EmployeeEditViewModel MapEmployeeToEmployeeCreateViewModel(Employee empl)
        {
            return new EmployeeEditViewModel()
            {
                Id = empl.Id,
                DepartmentId = empl.Department.Id,
                FirstName = empl.FirstName,
                LastName = empl.LastName,
                MiddleName = empl.MiddleName,
                MaritalStatusId = empl.MaritalStatus.Id,
                WorkStatusId = empl.WorkStatus.Id,
                Address = empl.Address,
                BeginningWork = empl.BeginningWork,
                EndWork = empl.EndWork,
                ImagePath = empl.ImagePath,
                Phones = empl.Phones,
                Emails = empl.Emails
            };
        }

        private Employee MapEmployeeEditViewModelToEmployee(EmployeeEditViewModel model)
        {
            return new Employee()
            {
                Id = model.Id,
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
                MaritalStatus = new MaritalStatus { Id = model.MaritalStatusId }
            };
        }


        #endregion
    }
}