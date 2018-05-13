using DM.PR.Common.Entities;
using DM.PR.Data.Repositories;
using System.Collections.Generic;

namespace DM.PR.Business.Providers.Implement
{
    public class EmployeeProvider : IEmployeeProvider
    {
        #region Private

        private IEmployeeRepository _employeeRepository;

        #endregion

        #region Ctor

        public EmployeeProvider(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion

        #region GetAll

        public IReadOnlyCollection<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        #endregion

        #region GetAllByDepartmentId

        public IReadOnlyCollection<Employee> GetAllByDepartmentId(int id)
        {
            return _employeeRepository.GetAllByDepartmentId(id);
        }

        #endregion

        #region GetAllShortModels

        public IReadOnlyCollection<EmployeeShortModel> GetAllShortModels()
        {
            return _employeeRepository.GetAllShortModels();
        }

        #endregion

        #region GetAllShortModelsByDepartmentId

        public IReadOnlyCollection<EmployeeShortModel> GetAllShortModelsByDepartmentId(int id)
        {
            return _employeeRepository.GetAllShortModelsByDepartmentId(id);
        }

        #endregion

        #region GetById

        public Employee GetById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        #endregion

    }
}

