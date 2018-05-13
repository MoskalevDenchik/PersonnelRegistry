using DM.PR.Common.Entities;
using System.Collections.Generic;

namespace DM.PR.Business.Providers
{
    public interface IDepartmentProvider
    {
        IReadOnlyCollection<Department> GetAll();

        Department GetById(int id);                            
    }
}
