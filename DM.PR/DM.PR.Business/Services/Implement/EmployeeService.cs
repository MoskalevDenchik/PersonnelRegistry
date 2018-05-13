using DM.PR.Common.Entities;
using DM.PR.Data.Repositories;

namespace DM.PR.Business.Services.Implement
{
    public class EmployeeService : IEmployeeService
    {
        #region Private

        private IEmployeeRepository _employeeRepository;

        #endregion

        #region Ctor

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion

        #region Create

        public void Create(Employee employee)
        {
            _employeeRepository.Create(employee);
        }

        #endregion

        #region Delete
        public void Delete(int id)
        {
            _employeeRepository.Delete(id);
        }

        #endregion

        #region Edit

        public void Edit(Employee employee)
        {
            _employeeRepository.Update(employee);
        }

        #endregion

    }
}
