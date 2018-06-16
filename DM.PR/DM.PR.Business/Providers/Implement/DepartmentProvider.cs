using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Providers.Implement
{
    internal class DepartmentProvider : Provider<Department>, IDepartmentProvider
    {
        private readonly IRepository<Department> _rep;
        private readonly IDepartmentSpecificationCreator _specificationCreator;

        public DepartmentProvider(IRepository<Department> rep, IDepartmentSpecificationCreator creater) : base(rep)
        {
            Inspector.ThrowExceptionIfNull(rep, creater);
            _rep = rep;
            _specificationCreator = creater;
        }

        public IReadOnlyCollection<Department> GetDepartments(int pageSize, int pageNumber, out int totalCount)
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
