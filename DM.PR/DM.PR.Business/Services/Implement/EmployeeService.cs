using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Services.Implement
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _rep;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            Helper.ThrowExceptionIfNull(employeeRepository);
            _rep = employeeRepository;
        }

        public void Create(Employee employee)
        {
            _rep.Add(employee);
        }

        public void Delete(int id)
        {
            _rep.Remove(id);
        }

        public void Edit(Employee employee)
        {
            _rep.Update(employee);
        }
    }
}
