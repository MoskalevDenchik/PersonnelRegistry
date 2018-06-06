using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System;

namespace DM.PR.Business.Providers.Implement
{
    internal class DepartmentProvider : IDepartmentProvider
    {
        private readonly IRepository<Department> _rep;
        private readonly IDepartmentSpecificationCreator _creator;

        public DepartmentProvider(IRepository<Department> rep, IDepartmentSpecificationCreator creater)
        {
            Helper.ThrowExceptionIfNull(rep, creater);
            _rep = rep;
            _creator = creater;
        }

        public Department GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Неверный ID");
            }

            return _rep.GetById(id);
        }

        public IReadOnlyCollection<Department> GetAll()
        {
            return _rep.GetAll();
        }

        public PagedData<Department> GetPage(int pageSize, int pageNumber)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                throw new Exception("Id пришел 0");
            }

            ISpecification findByPageSpecification = _creator.CreateFindByPageDataSpecification(pageSize, pageNumber);
            return _rep.FindPageBy(findByPageSpecification);
        }
    }
}
           