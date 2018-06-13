using DM.PR.WEB.Infrastructure.Attributes;
using System.Collections.Generic;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.WEB.Models;
using System.Web.Mvc;
using DM.PR.WEB.Models.Department;

namespace DM.PR.WEB.Controllers
{
    public class DepartmentsController : Controller
    {
        #region Private

        private readonly IDepartmentProvider _departmentProv;
        private readonly IDepartmentService _departmentServ;

        #endregion

        #region Ctors

        public DepartmentsController(IDepartmentProvider departmentProv, IDepartmentService departmentServ)
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
            var parent = department.ParentId != null ? _departmentProv.GetById((int)department.ParentId) : null;

            var model = MapDepartmentToDepartmentDetailsViewModel(department, parent);
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            var model = _departmentProv.GetById(id);
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
            var model = _departmentProv.GetPage(pageSize, pageNumber, out int totalPage);
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
            var list = new List<DepartmentSelectModel>();
            foreach (var item in departments)
            {
                list.Add(new DepartmentSelectModel()
                {
                    Id = (int)item.Id,
                    Name = item.Name
                });
            }
            return list;
        }


        private DepartmentDeatailsViewModel MapDepartmentToDepartmentDetailsViewModel(Department department, Department parent)
        {
            return new DepartmentDeatailsViewModel()
            {
                Id = department.Id,
                ParentId = department.ParentId,
                ParentName = parent?.Name,
                Name = department.Name,
                Address = department.Address,
                Description = department.Description,
                Phones = department.Phones
            };
        }

        public Department MapDepartmentCreateToDepartment(DepartmentCreateViewModel department)
        {
            return new Department()
            {
                Name = department.Name,
                ParentId = department.ParentId,
                Address = department.Address,
                Description = department.Description,
                Phones = department.Phones,
            };
        }

        public Department MapDepartmentEditToDepartment(DepartmentEditViewModel model)
        {
            return new Department()
            {
                Id = model.Id,
                Name = model.Name,
                ParentId = model.ParentId,
                Address = model.Address,
                Description = model.Description,
                Phones = model.Phones,
            };
        }

        #endregion
    }
}


