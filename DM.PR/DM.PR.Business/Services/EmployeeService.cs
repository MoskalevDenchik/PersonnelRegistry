using DM.PR.Business.Interfaces;
using DM.PR.Common.Entities;
using DM.PR.Data.Intefaces;

namespace DM.PR.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Create(Employee employee)
        {
            _employeeRepository.Create(employee);
        }

        public void Delete(int? id)
        {
            _employeeRepository.Delete(id);
        }

        public void Edit(Employee employee)
        {
            _employeeRepository.Update(employee);
        }
    }
}
