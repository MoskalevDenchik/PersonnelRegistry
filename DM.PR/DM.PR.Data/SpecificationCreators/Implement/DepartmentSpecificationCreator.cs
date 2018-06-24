using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Helpers;
using DM.PR.Data.Entities;

namespace DM.PR.Data.Specifications
{
    internal class DepartmentSpecificationCreator : IDepartmentSpecificationCreator
    {
        private IDepartmentParameterCreater _paramCreator;

        public DepartmentSpecificationCreator(IDepartmentParameterCreater paramCreator)
        {
            Inspector.ThrowExceptionIfNull(paramCreator);
            _paramCreator = paramCreator;
        }

        public ISpecification CreateSpecification(int PageSize, int Page)
        {
            return new Specification(_paramCreator.CreateFind(PageSize, Page));
        }

        public ISpecification CreateSpecification(int parentId)
        {
            return new Specification(_paramCreator.CreateFind(parentId));
        }
        public ISpecification CreateSpecification(string name)
        {
            return new Specification(_paramCreator.CreateFind(name));
        }
    }
}
