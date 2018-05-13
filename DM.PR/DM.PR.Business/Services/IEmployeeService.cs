using DM.PR.Common.Entities;

namespace DM.PR.Business.Services
{
    public interface IEmployeeService
    {
        void Create(Employee employee);
        void Edit(Employee employee);
        void Delete(int id);
    }
}
