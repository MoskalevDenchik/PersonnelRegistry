using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Services;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Services.Implement
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _rep;
        private readonly IСacheStorage _caching;

        public EmployeeService(IRepository<Employee> employeeRepository, IСacheStorage caching)
        {
            Inspector.ThrowExceptionIfNull(employeeRepository);
            _rep = employeeRepository;
            _caching = caching;
        }

        public void Create(Employee employee)
        {
            _rep.Add(employee);    
        }

        public void Delete(int id)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(id);
            _rep.Remove(id);
            CleanCach();
        }

        public void Edit(Employee employee)
        {
            _rep.Update(employee);
            CleanCach();
        }

        #region Helpers

        private void CleanCach()
        {
            _caching.DeleteWhoContains("Employees");
        }

        #endregion

    }
}
