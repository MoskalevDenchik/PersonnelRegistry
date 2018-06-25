using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Linq;

namespace DM.PR.Business.Providers.Implement
{
    internal class DepartmentProvider : Provider<Department>, IDepartmentProvider
    {
        #region Private

        private readonly IRepository<Department> _rep;
        private readonly IDepartmentSpecificationCreator _specificationCreator;

        #endregion

        #region Ctors
        public DepartmentProvider(IRepository<Department> rep, IDepartmentSpecificationCreator creater) : base(rep)
        {
            Inspector.ThrowExceptionIfNull(rep, creater);
            _rep = rep;
            _specificationCreator = creater;
        }

        #endregion

        public Department GetByName(string name)
        {
            ISpecification specification = _specificationCreator.CreateSpecification(name);
            return _rep.FindBy(specification).FirstOrDefault();
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

        public IReadOnlyCollection<Department> GetDepartments(int parentId)
        {
            if (parentId < 0)
            {
                return null;
            }

            ISpecification specification = _specificationCreator.CreateSpecification(parentId);
            return _rep.FindBy(specification);
        }
    }
}
