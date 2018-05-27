using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System.Collections.Generic;

namespace DM.PR.Data.Repositories
{
    public interface IEmployeeRepository
    {
        IReadOnlyCollection<Employee> GetAll();

        IReadOnlyCollection<Employee> GetAllByDepartmentId(int id);

        Employee GetById(int id);

        ExecuteResult Create(Employee item);

        ExecuteResult Update(Employee item);

        ExecuteResult Delete(int id);
    }
}
