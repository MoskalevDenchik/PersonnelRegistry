using DM.PR.Common.Entities;

namespace DM.PR.Business.Interfaces
{
    public interface IDepartmentService
    {
        void Delete(int? id);
        void Edit(Department department);
        void Create(Department department);
    }
}
