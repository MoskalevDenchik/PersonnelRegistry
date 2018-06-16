using System.Collections.Generic;
using DM.PR.Common.Entities;

namespace DM.PR.Business.Providers
{
    public interface IEmployeeProvider : IProvider<Employee>
    {
         IReadOnlyCollection<Employee> GetEmployees(int pageSize, int page, out int totalCount);
         IReadOnlyCollection<Employee> GetEmployees(int departmentId, int pageSize, int page, out int totalCount);
         IReadOnlyCollection<Employee> GetEmployees(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page, out int totalCount);
    }
}
