using DM.PR.Common.Entities;
using System.Collections.Generic;

namespace DM.PR.Data.Repositories
{
    public interface IEmployeeRepository
    {
        IReadOnlyCollection<Employee> GetAll();

        IReadOnlyCollection<Employee> GetAllByDepartmentId(int id);

        IReadOnlyCollection<EmployeeShortModel> GetAllShortModels();

        IReadOnlyCollection<EmployeeShortModel> GetAllShortModelsByDepartmentId(int id);

        Employee GetById(int id);

        int Create(Employee item);

        int Update(Employee item);

        int Delete(int id);
    }
}
