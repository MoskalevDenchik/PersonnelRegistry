using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.WEB.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class DepartmentsController : Controller
    {
        #region Private

        private IDepartmentProvider _departmentProvider;
        private IDepartmentService _departmentService;

        #endregion

        #region Ctors
        public DepartmentsController(IDepartmentProvider departmentProvider, IDepartmentService departmentService)
        {
            _departmentProvider = departmentProvider;
            _departmentService = departmentService;
        }
        #endregion

        #region Index
        public ActionResult Index() => View();

        #endregion

        #region List
        public PartialViewResult List(int? id)
        {
            return PartialView(_departmentProvider.GetAll());
        }
        #endregion

        #region Deatails
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                var department = _departmentProvider.GetById((int)id);
                if (department != null)
                {
                    var parentName = department.ParentId == null ?
                        null : _departmentProvider.GetById((int)department.ParentId).Name;

                    var departmentView = MapDepartmentToDepartmentDetails(department, parentName);

                    return View(departmentView);
                }
                else return HttpNotFound(); // Ошибка соединения с БД 
            }
            else return HttpNotFound();  // Ошибка пришел NULL
        }

        #endregion

        #region Edit
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                return View(_departmentProvider.GetById((int)id));
            }

            return View();// Дописать исключение
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Edit(department);
                return RedirectToAction("Index");
            }
            return View();   //Дописать перенаправления
        }
        #endregion

        #region Create

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
                _departmentService.Create(MapDepartmentCreateToDepartment(model));
                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                _departmentService.Delete((int)id);
                return RedirectToAction("Index");
            }

            return View(); // Посмотрть перенаправвление и конфликты
        }
        #endregion

        #region Helpers

        public PartialViewResult AddPhone(int phones)
        {
            return PartialView(phones);
        }

        public PartialViewResult SelectList()
        {
            var list = _departmentProvider.GetAll();
            if (list != null)
            {
                return PartialView(MapDepartmentToDepartmentSelectModel(list));
            }
            else return null;
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
