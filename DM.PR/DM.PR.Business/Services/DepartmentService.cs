using DM.PR.Business.Interfaces;
using DM.PR.Common.Entities;
using DM.PR.Data.Intefaces;         

namespace DM.PR.Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository) => _departmentRepository = departmentRepository;

        public void Delete(int? id) => _departmentRepository.Delete(id);

        public void Edit(Department department) => _departmentRepository.Update(department);

        public void Create(Department department) => _departmentRepository.Create(department);

    }
}

                                                         