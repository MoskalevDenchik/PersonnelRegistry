using DM.PR.WEB.Infrastructure.Attributes;
using DM.PR.WEB.Models.Department;
using System.Collections.Generic;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.WEB.Models;
using System.Web.Mvc;
using System.Linq;

namespace DM.PR.WEB.Controllers
{
    [Authorize(Roles = "admin,editor")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentProvider _departmentProv;
        private readonly IEntityService<Department> _departmentServ;

        public DepartmentsController(IDepartmentProvider departmentProv, IEntityService<Department> departmentServ)
        {
            Inspector.ThrowExceptionIfNull(departmentProv, departmentProv);
            _departmentProv = departmentProv;
            _departmentServ = departmentServ;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id = 0)
        {
            var department = _departmentProv.GetById(id);
            var parent = department.ParentId > 0 ? _departmentProv.GetById(department.ParentId) : null;

            var model = MapDepartmentToDepartmentDetailsViewModel(department, parent);
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            var department = _departmentProv.GetById(id);
            var model = MapDepartmentToDepartmentEditViewModel(department);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var department = MapDepartmentEditToDepartment(model);
            _departmentServ.Edit(department);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DepartmentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var department = MapDepartmentCreateToDepartment(model);
            _departmentServ.Create(department);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            _departmentServ.Delete(id);
            return RedirectToAction("Index");
        }

        #region Partial

        [AjaxOnly]
        public ActionResult GetAll(int pageSize, int pageNumber)
        {
            var model = _departmentProv.GetDepartments(pageSize, pageNumber, out int totalPage);
            return Json(new { Data = model, TotalCount = totalPage }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult GetParentDepartment()
        {
            var list = _departmentProv.GetAll();
            var model = MapDepartmentToDepartmentSelectModel(list);

            return PartialView("DepartmentSelect", model);
        }

        #endregion

        #region Mappers

        private IReadOnlyCollection<DepartmentSelectModel> MapDepartmentToDepartmentSelectModel(IReadOnlyCollection<Department> departments)
        {
            return departments.Select(d => new DepartmentSelectModel { Id = d.Id, Name = d.Name }).ToList();
        }

        private DepartmentDeatailsViewModel MapDepartmentToDepartmentDetailsViewModel(Department department, Department parent)
        {
            return new DepartmentDeatailsViewModel()
            {
                Id = department.Id,
                Name = department.Name,
                ParentName = parent?.Name,
                Phones = department.Phones,
                Address = department.Address,
                ParentId = department.ParentId,
                Description = department.Description
            };
        }

        public Department MapDepartmentCreateToDepartment(DepartmentCreateViewModel department)
        {
            return new Department()
            {
                Name = department.Name,
                Address = department.Address,
                ParentId = department.ParentId,
                Description = department.Description,
                Phones = department.Phones.Select(number => new Phone { Number = number }).ToList(),
            };
        }

        public Department MapDepartmentEditToDepartment(DepartmentEditViewModel model)
        {
            return new Department()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                ParentId = model.ParentId,
                Description = model.Description,
                Phones = model.Phones.Select(number => new Phone { Number = number }).ToList()
            };
        }

        public DepartmentEditViewModel MapDepartmentToDepartmentEditViewModel(Department department)
        {
            return new DepartmentEditViewModel()
            {
                Id = department.Id,
                Name = department.Name,
                Address = department.Address,
                ParentId = department.ParentId,
                Description = department.Description,
                Phones = department.Phones.Select(phone => phone.Number).ToList()
            };
        }

        #endregion
    }
}


