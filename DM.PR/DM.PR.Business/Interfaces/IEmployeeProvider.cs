using DM.PR.Common.Entities;
using System.Collections.Generic;

namespace DM.PR.Business.Interfaces
{
    public interface IEmployeeProvider
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> FindAllByDepartmentName(string name);
        Employee FindById(int? id);
    }
}
