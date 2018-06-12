using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Services.Implement
{
    internal class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _rep;

        public DepartmentService(IRepository<Department> departmentRepository)
        {
            Inspector.ThrowExceptionIfNull(departmentRepository);
            _rep = departmentRepository;
        }

        public void Create(Department department)
        {
            _rep.Add(department);
        }

        public void Edit(Department department)
        {
            _rep.Update(department);
        }

        public void Delete(int id)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(id);
            _rep.Remove(id);
        }
    }
}

