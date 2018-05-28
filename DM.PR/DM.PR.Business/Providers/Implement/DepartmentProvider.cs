using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.Data.Repositories;
using System;
using System.Collections.Generic;

namespace DM.PR.Business.Providers.Implement
{
    internal class DepartmentProvider : IDepartmentProvider
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentProvider(IDepartmentRepository departmentRepository)
        {
            Helper.ThrowExceptionIfNull(departmentRepository);
            _departmentRepository = departmentRepository;
        }

        public IReadOnlyCollection<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }

        public Department GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Неверный ID ");
            }
            return _departmentRepository.GetById(id);
        }
    }
}
