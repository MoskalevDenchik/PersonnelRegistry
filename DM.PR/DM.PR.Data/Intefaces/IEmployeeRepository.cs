using DM.PR.Common.Entities;
using System.Collections.Generic;

namespace DM.PR.Data.Intefaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee Get(int? id);
        void Create(Employee item);
        void Update(Employee item);
        void Delete(int? id);
    }
}
