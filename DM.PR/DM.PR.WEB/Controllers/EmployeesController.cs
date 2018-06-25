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

        private IEmployeeProvider _emplProv;
        private IDepartmentProvider _depProv;
        private IEntityService<Employee> _emplServ;
        private IProvider<WorkStatus> _workStatProv;
        private IProvider<MaritalStatus> _merStatProv;

        #endregion

        #region Ctors

        public EmployeesController(IEmployeeProvider employeeProvider, IEntityService<Employee> employeeService,
            IDepartmentProvider departmentProvider, IProvider<WorkStatus> workStatProv, IProvider<MaritalStatus> merStatProv)
        {
            Inspector.ThrowExceptionIfNull(employeeProvider, employeeService, departmentProvider, workStatProv, merStatProv);
            _depProv = departmentProvider;
            _emplProv = employeeProvider;
            _emplServ = employeeService;
            _merStatProv = merStatProv;
            _workStatProv = workStatProv;
        }
        #endregion

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            var workStatusList = _workStatProv.GetAll();
            return View(new EmployeeSearchModel { WorkStatusLst = workStatusList });
        }

        public ActionResult Navigation()
        {
            return View();
        }

        [Authorize(Roles = "admin,editor")]
        public ActionResult Details(int id = 0)
        {
            var empl = _emplProv.GetById(id);
            return View(MapToEmployeeDetailsViewModel(empl));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var marStatusList = _merStatProv.GetAll();
            var workStatusList = _workStatProv.GetAll();
            var departmentList = MapToDepartmentSelectModel(_depProv.GetAll());
            return View(new EmployeeSaveViewModel
            {
                WorkStatusList = workStatusList,
                MaritalStatusList = marStatusList,
                DepartmentList = departmentList
            });
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id = 0)
        {
            var empl = _emplProv.GetById(id);
            var model = MapToEmployeeSaveViewModel(empl);
            model.MaritalStatusList = _merStatProv.GetAll();
            model.WorkStatusList = _workStatProv.GetAll();
            model.DepartmentList = MapToDepartmentSelectModel(_depProv.GetAll());
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id = 0)
        {
            _emplServ.Remove(id);
            return RedirectToAction("Index");
        }

        #region Partial and Json

        [HttpPost]
        [AjaxOnly]
        [Authorize(Roles = "admin")]
        public JsonResult Save(Employee employee)
        {
            var result = _emplServ.Save(employee);
            return Json(result);
        }

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
            var list = _emplProv.GetEmployees(pageSize, pageNumber, out int totalCount);
            return Json(new { Data = list, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]                                                                                                                      
        public ActionResult GetEmployeesByDepartmentId(int departmentId, int pageNumber, int pageSize)
        {
            var list = _emplProv.GetEmployees(departmentId, pageSize, pageNumber, out int totalCount);
            ViewBag.totalCount = totalCount;
            var model = MapToEmployeesSummaryViewModel(list);
            return PartialView("EmployeeSummary", model);
        }

        [AjaxOnly]
        public ActionResult GetEmployees(string middleName, string firstName, string lastName, int pageNumber, int pageSize, int WorkStatusId = 0, int fromYear = 0, int toYear = 100)
        {
            var emplList = _emplProv.GetEmployees(lastName, firstName, middleName, fromYear, toYear, WorkStatusId, pageSize, pageNumber, out int totalCount);
            ViewBag.totalCount = totalCount;
            var model = MapToEmployeesSummaryViewModel(emplList);
            return PartialView("EmployeeSummary", model);
        }
        #endregion

        #region Mappers

        private IReadOnlyCollection<DepartmentSelectModel> MapToDepartmentSelectModel(IReadOnlyCollection<Department> departments)
        {
            return departments.Select(d => new DepartmentSelectModel { Id = d.Id, Name = d.Name }).ToList();
        }

        private IReadOnlyCollection<EmployeeSummaryViewModel> MapToEmployeesSummaryViewModel(IReadOnlyCollection<Employee> list)
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

        private EmployeeDetailsViewModel MapToEmployeeDetailsViewModel(Employee empl) => new EmployeeDetailsViewModel
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

        private EmployeeSaveViewModel MapToEmployeeSaveViewModel(Employee empl) => new EmployeeSaveViewModel
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