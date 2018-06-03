using DM.PR.Common.Entities;
using System;
using System.Collections.Generic;

namespace DM.PR.Business.Providers
{
    public interface IEmployeeProvider
    {
        Employee GetById(int id);

        IReadOnlyCollection<Employee> FindBy(string MiddledName, string FirstName, string LastName, DateTime? WorkTime, bool IsWorking);

        PagedData<Employee> GetAll(int pageSize, int page);

        IReadOnlyCollection<Employee> GetAllByDepartmentId(int id);
    }
}
