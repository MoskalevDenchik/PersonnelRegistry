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

        private readonly IDepartmentProvider _depProv;
        private readonly IEntityService<Department> _depServ;

        #endregion

        #region Ctors

        public DepartmentsController(IDepartmentProvider departmentProv, IEntityService<Department> departmentServ)
        {
            Inspector.ThrowExceptionIfNull(departmentProv, departmentProv);
            _depProv = departmentProv;
            _depServ = departmentServ;
        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id = 0)
        {
            var department = _depProv.GetById(id);
            var parent = department.ParentId > 0 ? _depProv.GetById(department.ParentId) : null;
            var model = MapDepartmentToDepartmentDetailsViewModel(department, parent);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            var department = _depProv.GetById(id);
            return View(MapDepartmentToDepartmentEditViewModel(department));
        }

        [AjaxOnly]
        [HttpPost]
        public JsonResult Save(Department department)
        {
            var result = _depServ.Save(department);
            return Json(result);
        }

        public ActionResult Delete(int id = 0)
        {
            _depServ.Remove(id);
            return RedirectToAction("Index");
        }

        #region Partial

        [AjaxOnly]
        public ActionResult GetAll(int pageSize, int pageNumber)
        {
            var model = _depProv.GetDepartments(pageSize, pageNumber, out int totalPage);
            return Json(new { Data = model, TotalCount = totalPage }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult GetParentDepartment(int selectedId)
        {
            ViewBag.departmentId = selectedId;
            var list = _depProv.GetAll();
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

        public DepartmentSaveViewModel MapDepartmentToDepartmentEditViewModel(Department department)
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


