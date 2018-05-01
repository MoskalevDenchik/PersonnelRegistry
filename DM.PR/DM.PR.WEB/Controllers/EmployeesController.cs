using DM.PR.Business.Interfaces;
using DM.PR.Business.Providers;
using DM.PR.Common.Entities;
using DM.PR.WEB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeeProvider _employeeProvider;
        //---------------------------------------Ctor--------------------------------------------------------------
        public EmployeesController(IEmployeeProvider employeeProvider)
        {
            _employeeProvider = employeeProvider;
        }
        //--------------------------------------Index--------------------------------------------------------------
        public ActionResult Index()
        {
            return View();
        }

        //--------------------------------------List---------------------------------------------------------------
        public PartialViewResult List(string department)
        {
            var list = new List<EmployeeListViewModel>();
            IEnumerable<Employee> list2;

            if (department == null) { list2 = _employeeProvider.GetAll(); }
            else { list2 = _employeeProvider.FindAllByDepartmentName(department); }

            foreach (var item in list2)
            {
                list.Add(MapEmployeeToEmployeeListViewModel(item));
            }

            return PartialView(list);
        }

        //--------------------------------------Create-------------------------------------------------------------
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {

            }

            return View();
        }

        //--------------------------------------Details------------------------------------------------------------
        public ActionResult Details(int? id)
        {
            return View(_employeeProvider.FindById(id));
        }

        //--------------------------------------Delete-------------------------------------------------------------
        public ActionResult Delete(int Id)
        {
            return View();
        }

        //-------------------------------------

        #region Mappers
        public EmployeeListViewModel MapEmployeeToEmployeeListViewModel(Employee employee)
        {
            return new EmployeeListViewModel()
            {
                Id = employee.Id,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                FirstName = employee.FirstName,
                DepartmentName = employee.Department.Name,
                WorkPhone = employee.Phones
            };
        }


        #endregion

    }
}