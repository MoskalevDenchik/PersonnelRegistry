using DM.PR.Business.Interfaces;
using DM.PR.Common.Entities;
using DM.PR.Common.Enums;
using DM.PR.Data.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.PR.Business.Providers
{
    public class EmployeeProvider : IEmployeeProvider
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeProvider(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        public IEnumerable<Employee> FindAllByDepartmentName(string name)
        {
            return _employeeRepository.GetAll()
                .Where(d => d.Department.Name == name)
                .Select(d => d)
                .ToList();
        }

        public Employee FindById(int? id)
        {
            return _employeeRepository.Get(id);
        }
    }
}

