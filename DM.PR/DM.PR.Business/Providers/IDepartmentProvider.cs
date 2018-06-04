using DM.PR.Common.Entities;
using System.Collections.Generic;

namespace DM.PR.Business.Providers
{
    public interface IDepartmentProvider
    {
        Department GetById(int id);
        PagedData<Department> GetAll(int pageSize, int pageNumber);
        IReadOnlyCollection<Department> GetAll();
    }
}
