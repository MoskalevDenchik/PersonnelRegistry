using DM.PR.Business.Interfaces;
using DM.PR.Common.Entities;   
using DM.PR.Data.Intefaces;
using System.Collections.Generic;  
using System.Linq;    

namespace DM.PR.Business.Providers
{
    public class DepartmentProvider : IDepartmentProvider
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentProvider(IDepartmentRepository departmentRepository) => _departmentRepository = departmentRepository;

        public IEnumerable<Department> GetAll() => _departmentRepository.GetAll();

        public IEnumerable<string> GetListOfName() => _departmentRepository.GetAll().Select(d => d.Name);

        public Department GetById(int? id) => _departmentRepository.Get(id);

        public IEnumerable<DepartmentNavModel> GetAllAsNavModel() => _departmentRepository.GetAllAsNavModel();
    }
}
