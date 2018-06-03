using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System;
using System.Collections.Generic;

namespace DM.PR.Data.Repositories
{
    public interface IEmployeeRepository
    {
        PagedData<Employee> GetAll(int pageSize, int page);

        IReadOnlyCollection<Employee> GetAllByDepartmentId(int id);

        IReadOnlyCollection<Employee> FindBy(string MiddledName, string FirstName, string LastName, DateTime? WorkTime, bool IsWorking);

        Employee GetById(int id);

        ExecuteResult Create(Employee item);

        ExecuteResult Update(Employee item);

        ExecuteResult Delete(int id);
    }
}
