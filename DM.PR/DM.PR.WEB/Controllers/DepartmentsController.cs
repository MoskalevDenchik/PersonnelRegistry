using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.WEB.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentProvider _departmentProv;
        private readonly IDepartmentService _departmentServ;

        public DepartmentsController(IDepartmentProvider departmentProv, IDepartmentService departmentServ)
        {
            Helper.ThrowExceptionIfNull(departmentProv, departmentProv);
            _departmentProv = departmentProv;
            _departmentServ = departmentServ;
        }

        public ActionResult Index()
        {
            return HttpNotFound();
            return View();
            
        }

        public PartialViewResult List(int id = 0)
        {
            return PartialView(_departmentProv.GetAll());
        }

        public ActionResult Details(int id = 0)
        {
            var department = _departmentProv.GetById(id);

            var parentName = department.ParentId == null ?
                    null : _departmentProv.GetById((int)department.ParentId).Name;

            var departmentView = MapDepartmentToDepartmentDetails(department, parentName);

            return View(departmentView);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            return View(_departmentProv.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            _departmentServ.Edit(department);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var department = new DepartmentCreateViewModel
            {
                Phones = new List<Phone>()
            };

            return View(department);
        }

        [HttpPost]
        public ActionResult Create(DepartmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View();
            }

            _departmentServ.Create(MapDepartmentCreateToDepartment(model));
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            _departmentServ.Delete(id);
            return RedirectToAction("Index");
        }

        #region Helpers     

        public ActionResult SelectList()
        {
            var list = _departmentProv.GetAll();
            if (list != null)
            {
                return PartialView(MapDepartmentToDepartmentSelectModel(list));
            }
            else
            {
                return RedirectToAction("ServerError", "Error");
            }
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


        private DepartmentDeatailsViewModel MapDepartmentToDepartmentDetails(Department department, string parentName)
        {
            return new DepartmentDeatailsViewModel()
            {
                Id = department.Id,
                ParentId = department.ParentId,
                ParentName = parentName,
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

        #endregion
    }
}
