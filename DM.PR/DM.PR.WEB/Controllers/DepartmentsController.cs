using DM.PR.Business.Interfaces;
using DM.PR.Common.Entities;
using DM.PR.WEB.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class DepartmentsController : Controller
    {
        private IDepartmentProvider _departmentProvider;
        private IDepartmentService _departmentService;

        //---------------------------------------Ctor--------------------------------------------------------------
        public DepartmentsController(IDepartmentProvider departmentProvider, IDepartmentService departmentService)
        {
            _departmentProvider = departmentProvider;
            _departmentService = departmentService;
        }

        //--------------------------------------Index--------------------------------------------------------------
        public ActionResult Index()
        {
            return View();
        }

        //--------------------------------------List---------------------------------------------------------------
        public PartialViewResult List()
        {
            var list = new List<DepartmentListViewModel>();
            foreach (var item in _departmentProvider.GetAll())
            {
                list.Add(MapDepartmentToDepartmetListViewModel(item));
            }
            return PartialView(list);
        }

        //--------------------------------------Menu--------------------------------------------------------------
        public PartialViewResult Menu()
        {
            return PartialView(_departmentProvider.GetListOfName());
        }

        //--------------------------------------Details------------------------------------------------------------
        public ActionResult Details(string department)
        {
            if (department != null)
            {
                return View(_departmentProvider.FindByName(department));
            }
            return View();
        }

        //---------------------------------------Edit--------------------------------------------------------------
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

        //--------------------------------------Delete-------------------------------------------------------------
        public ActionResult Delete(int? id)
        {
            _departmentService.Delete(id);
            return RedirectToAction("Index");  // Посмотрть перенаправвление и конфликты
        }

        #region Mappers
        public DepartmentListViewModel MapDepartmentToDepartmetListViewModel(Department department)
        {
            return new DepartmentListViewModel()
            {
                Id = department.Id,
                Name = department.Name,
                Address = department.Address,
                Description = department.Description,
                Phones = department.Phones,
                EmployeeQuantity = 1
            };
        }
    }


    #endregion
}
