using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.Data.Repositories;
using System.Collections.Generic;

namespace DM.PR.Business.Providers.Implement
{
    internal class EmployeeProvider : IEmployeeProvider
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeProvider(IEmployeeRepository employeeRepository)
        {
            Helper.ThrowExceptionIfNull(employeeRepository);
            _employeeRepository = employeeRepository;
        }

        public IReadOnlyCollection<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        public Employee GetById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public IReadOnlyCollection<Employee> GetAllByDepartmentId(int id)
        {
            if (id > 0)
            {
                return _employeeRepository.GetAllByDepartmentId(id);
            }
            else
            {
                return _employeeRepository.GetAll();
            }
        }
    }
}

