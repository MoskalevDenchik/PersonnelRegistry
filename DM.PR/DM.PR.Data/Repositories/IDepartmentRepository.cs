using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System.Collections.Generic;   

namespace DM.PR.Data.Repositories
{
    public interface IDepartmentRepository
    {
        IReadOnlyCollection<Department> GetAll();

        Department GetById(int id);

        ExecuteResult Create(Department item);

        ExecuteResult Update(Department item);

        ExecuteResult Delete(int id);                                         
    }
}
