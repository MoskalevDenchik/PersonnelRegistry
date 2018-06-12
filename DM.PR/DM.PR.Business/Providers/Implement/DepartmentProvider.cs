using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Providers.Implement
{
    internal class DepartmentProvider : IDepartmentProvider
    {
        private readonly IRepository<Department> _rep;
        private readonly IDepartmentSpecificationCreator _specificationCreator;

        public DepartmentProvider(IRepository<Department> rep, IDepartmentSpecificationCreator creater)
        {
            Inspector.ThrowExceptionIfNull(rep, creater);
            _rep = rep;
            _specificationCreator = creater;
        }

        public Department GetById(int id)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(id);
            return _rep.GetById(id);
        }

        public IReadOnlyCollection<Department> GetAll()
        {
            return _rep.GetAll();
        }

        public IReadOnlyCollection<Department> GetPage(int pageSize, int pageNumber, out int totalCount)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(pageSize, pageNumber);

            ISpecification findByPageSpecification = _specificationCreator.CreateFindByPageDataSpecification(pageSize, pageNumber);
            return _rep.FindBy(findByPageSpecification, out totalCount);
        }
    }
}
