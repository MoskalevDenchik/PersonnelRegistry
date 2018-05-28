using DM.PR.Common.Entities;
using System.Collections.Generic;

namespace DM.PR.Business.Providers
{
    public interface IDepartmentProvider
    {
        Department GetById(int id);
        IReadOnlyCollection<Department> GetAll();
    }
}
