using System.Collections.Generic;
using DM.PR.Common.Entities;

namespace DM.PR.Business.Providers
{
    public interface IDepartmentProvider
    {
        Department GetById(int id);
        IReadOnlyCollection<Department> GetAll();
        IReadOnlyCollection<Department> GetDepartments(int pageSize, int pageNumber, out int totalCount);
    }
}
