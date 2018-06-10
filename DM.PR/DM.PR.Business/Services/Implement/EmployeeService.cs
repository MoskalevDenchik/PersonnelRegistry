using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Services;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Services.Implement
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _rep;
        private readonly IСachingService _caching;

        public EmployeeService(IRepository<Employee> employeeRepository, IСachingService caching)
        {
            Helper.ThrowExceptionIfNull(employeeRepository);
            _rep = employeeRepository;
            _caching = caching;
        }

        public void Create(Employee employee)
        {
            CleanCach();
            _rep.Add(employee);
        }

        public void Delete(int id)
        {
            CleanCach();
            _rep.Remove(id);
        }

        public void Edit(Employee employee)
        {
            CleanCach();
            _rep.Update(employee);
        }

        #region Helpers

        private void CleanCach()
        {
            _caching.DeleteWhoContains("Employees");
        }

        #endregion

    }
}
