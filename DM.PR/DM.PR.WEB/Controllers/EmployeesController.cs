using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.WEB.Models;
using DM.PR.WEB.Models.Employee;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class EmployeesController : Controller
    {
        #region Private

        private IEmployeeProvider _employeeProvider;
        private IEmployeeService _employeeService;
        private IDepartmentProvider _departmentProvider;
        private IMaritalStatusProvider _maritalStatusProvider;

        #endregion

        #region Ctors

        public EmployeesController(IEmployeeProvider employeeProvider,
            IEmployeeService employeeService,
            IDepartmentProvider departmentProvider,
            IMaritalStatusProvider maritalStatusProvider)
        {
            _employeeProvider = employeeProvider;
            _employeeService = employeeService;
            _departmentProvider = departmentProvider;
            _maritalStatusProvider = maritalStatusProvider;
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
            var employee = new EmployeeCreateViewModel
            {
                Emails = new List<Email>(),
                Phones = new List<Phone>()
            };
            return View(employee);
        }

        [HttpPost]
        public ActionResult Create(EmployeeCreateViewModel employee)
        {
            _employeeService.Create(MapEmployeeCreateViewModelToEmployee(employee));
            return RedirectToAction("Index");
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
                    var departmentName = "";

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

        #region Partials
 
        public PartialViewResult AddEmail(int emails)
        {
            return PartialView(emails);
        }

        public PartialViewResult AddPhone(int phones)
        {
            return PartialView(phones);
        }

        [ChildActionOnly]
        public PartialViewResult SelectMaritalStatus()
        {
            var list = _maritalStatusProvider.GetAll();
            if (list != null)
            {
                return PartialView(list);
            }
            else
            {
                return null;
            }
        }

        [ChildActionOnly]
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

        EmployeeDetailsViewModel MapEmployeeToEmployeeDetailsViewModel(Employee employee, string departmentName)
        {
            return new EmployeeDetailsViewModel()
            {
                Id = employee.Id,                     
                DepartmentName = departmentName,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Address = employee.Address,
                BeginningOfWork = employee.BeginningWork,
                EndOfWork = employee.EndWork,
                ImagePath = employee.ImagePath,         
                Phones = employee.Phones,
                Emails = employee.Emails
            };
        }

        Employee MapEmployeeCreateViewModelToEmployee(EmployeeCreateViewModel employee)
        {
            return new Employee()
            {                                          
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Address = employee.Address,
                BeginningWork = employee.BeginningWork,
                EndWork = employee.EndWork,
                ImagePath = employee.ImagePath,               
                Phones = employee.Phones,
                Emails = employee.Emails
            };
        }
        #endregion

    }
}