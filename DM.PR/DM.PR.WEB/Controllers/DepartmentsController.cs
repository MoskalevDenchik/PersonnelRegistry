using DM.PR.WEB.Infrastructure.Attributes;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.WEB.Models;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

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
            var model = MapToDepartmentDetailsViewModel(department, parent);
            return View(model);
        }

        public ActionResult Create()
        {
            var depList = _depProv.GetAll();
            return View(new DepartmentSaveViewModel
            {
                DepartmentList = depList.Select(x => new DepartmentSelectModel { Id = x.Id, Name = x.Name }).ToList()
            });
        }

        public ActionResult Edit(int id = 0)
        {
            var dep = _depProv.GetById(id);
            var depList = _depProv.GetAll();
            return View(MapToDepartmentEditViewModel(dep, depList));
        }

        public ActionResult Delete(int id = 0)
        {
            _depServ.Remove(id);
            return RedirectToAction("Index");
        }

        #region Partial

        [AjaxOnly]
        [HttpPost]
        public JsonResult Save(Department department) => Json(_depServ.Save(department));

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
            return PartialView("DepartmentSelect", list.Select(x => new DepartmentSelectModel { Id = x.Id, Name = x.Name }));
        }

        #endregion

        #region Mappers

        private DepartmentDeatailsViewModel MapToDepartmentDetailsViewModel(Department department, Department parent) => new DepartmentDeatailsViewModel()
        {
            Id = department.Id,
            Name = department.Name,
            ParentName = parent?.Name,
            Phones = department.Phones,
            Address = department.Address,
            ParentId = department.ParentId,
            Description = department.Description
        };

        public DepartmentSaveViewModel MapToDepartmentEditViewModel(Department dep, IReadOnlyCollection<Department> depList) => new DepartmentSaveViewModel()
        {
            Id = dep.Id,
            Name = dep.Name,
            Address = dep.Address,
            ParentId = dep.ParentId,
            Description = dep.Description,
            Phones = dep.Phones.ToArray(),
            DepartmentList = depList.Select(x => new DepartmentSelectModel { Id = x.Id, Name = x.Name }).ToList()
        };

        #endregion
    }
}


