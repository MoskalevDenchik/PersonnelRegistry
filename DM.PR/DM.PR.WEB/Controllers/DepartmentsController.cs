using DM.PR.WEB.Infrastructure.Attributes;
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
        #region Private

        private readonly IDepartmentProvider _departmentProv;
        private readonly IEntityService<Department> _departmentServ;

        #endregion

        #region Ctors

        public DepartmentsController(IDepartmentProvider departmentProv, IEntityService<Department> departmentServ)
        {
            Inspector.ThrowExceptionIfNull(departmentProv, departmentProv);
            _departmentProv = departmentProv;
            _departmentServ = departmentServ;
        }

        #endregion

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

        public ActionResult Create()
        {

            ViewBag.title = "Добавьте отдел";
            return View("Save", new DepartmentSaveViewModel { });
        }

        [HttpPost]
        public ActionResult Create(DepartmentSaveViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var department = MapDepartmentSaveViewModelToDepartment(model);
            _departmentServ.Save(department);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.title = "Редактируйте отдел";
            var department = _departmentProv.GetById(id);
            var model = MapDepartmentToDepartmentSaveViewModel(department);
            return View("Save", model);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentSaveViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var department = MapDepartmentSaveViewModelToDepartment(model);
            _departmentServ.Save(department);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id = 0)
        {
            _departmentServ.Remove(id);
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
        public ActionResult GetParentDepartment(int selectedId)
        {
            ViewBag.departmentId = selectedId;
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
      

        public Department MapDepartmentSaveViewModelToDepartment(DepartmentSaveViewModel model)
        {
            return new Department()
            {
                Name = model.Name,
                Address = model.Address,
                ParentId = model.ParentId,
                Description = model.Description,
            };
        }

        public DepartmentSaveViewModel MapDepartmentToDepartmentSaveViewModel(Department department)
        {
            return new DepartmentSaveViewModel()
            {
                Name = department.Name,
                Address = department.Address,
                ParentId = department.ParentId,
                Description = department.Description
            };
        }

        #endregion
    }
}


