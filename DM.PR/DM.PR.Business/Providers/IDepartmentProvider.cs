using System.Collections.Generic;
using DM.PR.Common.Entities;

namespace DM.PR.Business.Providers
{
    public interface IDepartmentProvider : IProvider<Department>
    {
        Department GetByName(string name);
        IReadOnlyCollection<Department> GetDepartments(int parentId);
        IReadOnlyCollection<Department> GetDepartments(int pageSize, int pageNumber, out int totalCount);
    }
}
