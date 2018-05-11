using DM.PR.Common.Entities;   
using System.Collections.Generic;   

namespace DM.PR.Data.Intefaces
{
    public interface IDepartmentRepository
    {
        IReadOnlyCollection<Department> GetAll();
        Department Get(int? id);
        void Create(Department item);
        void Update(Department item);
        void Delete(int? id);
        IReadOnlyCollection<DepartmentNavModel> GetAllAsNavModel();
    }
}
