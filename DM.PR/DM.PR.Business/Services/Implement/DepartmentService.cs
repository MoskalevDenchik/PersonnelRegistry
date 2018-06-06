using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System;

namespace DM.PR.Business.Services.Implement
{
    internal class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _rep;

        public DepartmentService(IRepository<Department> departmentRepository)
        {
            Helper.ThrowExceptionIfNull(departmentRepository);
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
            if (id <= 0)
            {
                throw new Exception("Неверный ID");
            }
            _rep.Remove(id);
        }
    }
}

