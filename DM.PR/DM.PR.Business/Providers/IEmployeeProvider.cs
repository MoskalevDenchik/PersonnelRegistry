using DM.PR.Common.Entities;
using System.Collections.Generic;

namespace DM.PR.Business.Providers
{
    public interface IEmployeeProvider
    {
        Employee GetById(int id);

        IReadOnlyCollection<Employee> GetAll();

        IReadOnlyCollection<Employee> GetAllByDepartmentId(int id);

        IReadOnlyCollection<EmployeeShortModel> GetAllShortModels();

        IReadOnlyCollection<EmployeeShortModel> GetAllShortModelsByDepartmentId(int id);
        
    }
}
