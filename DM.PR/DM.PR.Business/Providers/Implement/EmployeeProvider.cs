using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.Data.Repositories;
using System;
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

        public PagedData<Employee> GetAll(int pageSize, int page)
        {
            return _employeeRepository.GetAll(pageSize, page);
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
                return null;//return _employeeRepository.GetAll(2, 4);
            }
        }

        public IReadOnlyCollection<Employee> FindBy(string MiddledName, string FirstName, string LastName, DateTime? WorkTime, bool IsWorking)
        {
            return _employeeRepository.FindBy(MiddledName, FirstName, LastName, WorkTime, IsWorking);
        }
    }
}

