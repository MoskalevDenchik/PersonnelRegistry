using DM.PR.Common.Entities;
using System.Collections.Generic;

namespace DM.PR.Business.Interfaces
{
    public interface IDepartmentProvider
    {
        IEnumerable<Department> GetAll();
        IEnumerable<string> GetListOfName();

    }
}
