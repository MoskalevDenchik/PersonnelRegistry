using DM.PR.Common.Entities;

namespace DM.PR.Business.Services
{
    public interface IDepartmentService
    {
        void Create(Department department);
        void Edit(Department department);
        void Delete(int id);
    }
}
