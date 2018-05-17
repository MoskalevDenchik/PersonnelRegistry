using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.WEB.Models;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class EmployeesController : Controller
    {
        #region Private

        private IEmployeeProvider _employeeProvider;
        private IEmployeeService _employeeService;
        private IDepartmentProvider _departmentProvider;

        #endregion

        #region Ctors

        public EmployeesController(IEmployeeProvider employeeProvider, IEmployeeService employeeService, IDepartmentProvider departmentProvider)
        {
            _employeeProvider = employeeProvider;
            _employeeService = employeeService;
            _departmentProvider = departmentProvider;
        }
        #endregion

        #region Index

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region List

        public PartialViewResult List(int id = 0)
        {
            var model = _employeeProvider.GetAllByDepartmentId(id);
            if (model != null)
            {
                return PartialView(model);
            }
            else
            {
                return PartialView("NoEmployees");
            }
        }
        #endregion

        #region Create

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.Create(employee);
            }
            return View();
        }

        #endregion

        #region Deatails

        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                var employee = _employeeProvider.GetById((int)id);
                if (employee != null)
                {
                    var departmentName = employee.DepartmentId == null ?
                                            null : _departmentProvider.GetById(((int)employee.DepartmentId)).Name;

                    var eployeeView = MapEmployeeToEmployeeDetailsViewModel(employee, departmentName);

                    return View(eployeeView);
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
                return View(_employeeProvider.GetById((int)id));
            }
            return View(); // написать Redirect

        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.Edit(employee);
            }
            return View();
        }

        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                _employeeService.Delete((int)id);
                return RedirectToAction("Index");
            }
            return View();
        }

        #endregion

        #region Mappers

        EmployeeDetailsViewModel MapEmployeeToEmployeeDetailsViewModel(Employee employee, string departmentName)
        {
            return new EmployeeDetailsViewModel()
            {
                Id = employee.Id,
                DepartmentId = employee.DepartmentId,
                DepartmentName = departmentName,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Address = employee.Address,
                BeginningOfWork = employee.BeginningOfWork,
                Emails = employee.Emails,
                EndOfWork = employee.EndOfWork,
                ImagePath = employee.ImagePath,
                MaritalStatus = employee.MaritalStatus,
                Phones = employee.Phones
            };
        }
        #endregion

    }
}