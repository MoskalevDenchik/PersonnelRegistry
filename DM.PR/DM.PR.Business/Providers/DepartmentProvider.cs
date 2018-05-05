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

        public DepartmentProvider(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }

        public IEnumerable<string> GetListOfName()
        {
            return _departmentRepository.GetAll().Select(d => d.Name).ToList();
        }

        public Department FindByName(string name)
        {
            return _departmentRepository.GetAll().FirstOrDefault(d => d.Name == name);
        }

    }
}
