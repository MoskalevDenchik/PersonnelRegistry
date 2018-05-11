using DM.PR.Business.Interfaces;
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
        public PartialViewResult List()
        {
            return PartialView(_departmentProvider.GetAll());
        }
        #endregion

        #region Deatails
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                return View(_departmentProvider.GetById(id));
            }
            return View();
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? id)
        {
            return View(_departmentProvider.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Edit(department);
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Create

        public ActionResult Create()
        {
            return View(new DepartmentCreateViewModel()
            {
                Phones = new List<Phone>() { new Phone() }
            });
        }


        [HttpPost]
        public ActionResult Create(FormCollection model)
        {

            if (ModelState.IsValid)
            {
                //_departmentService.Create(MapDepartmentCreateToDepartment(model));
                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {
            _departmentService.Delete(id);
            return RedirectToAction("Index");  // Посмотрть перенаправвление и конфликты
        }
        #endregion

        #region Mappers

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
