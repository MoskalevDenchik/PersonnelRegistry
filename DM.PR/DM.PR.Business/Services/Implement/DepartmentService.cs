using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.Data.Repositories;

namespace DM.PR.Business.Services.Implement
{
    internal class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            Helper.ThrowExceptionIfNull(departmentRepository);
            _departmentRepository = departmentRepository;
        }

        public void Create(Department department)
        {
            _departmentRepository.Create(department);
        }

        public void Delete(int id)
        {
            _departmentRepository.Delete(id);
        }

        public void Edit(Department department)
        {
            _departmentRepository.Update(department);
        }
    }
}

