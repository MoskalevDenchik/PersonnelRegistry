using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System;

namespace DM.PR.Business.Services.Implement
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _rep; 

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            Inspector.ThrowExceptionIfNull(employeeRepository);
            _rep = employeeRepository;    
        }

        public void Create(Employee employee)
        {
            _rep.Add(employee);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Invalid Id");
            }
            _rep.Remove(id);
        }

        public void Edit(Employee employee)
        {
            _rep.Update(employee);
        }
    }
}
