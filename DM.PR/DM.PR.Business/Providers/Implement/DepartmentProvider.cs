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
            if (id <= 0)
            {                                                               
                return null;
            }

            return _rep.GetById(id);
        }

        public IReadOnlyCollection<Department> GetAll()
        {
            return _rep.GetAll();
        }

        public IReadOnlyCollection<Department> GetPage(int pageSize, int pageNumber, out int totalCount)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                totalCount = 0;
                return null;
            }

            ISpecification specification = _specificationCreator.CreateSpecification(pageSize, pageNumber);
            return _rep.FindBy(specification, out totalCount);
        }
    }
}
