using DM.PR.Common.Entities;   
using System.Collections.Generic;   

namespace DM.PR.Data.Repositories
{
    public interface IDepartmentRepository
    {
        IReadOnlyCollection<Department> GetAll();

        Department GetById(int id);

        int Create(Department item);

        int Update(Department item);

        int Delete(int id);                                         
    }
}
