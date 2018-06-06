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
            Helper.ThrowExceptionIfNull(paramCreator);
            _paramCreator = paramCreator;
        }

        public ISpecification CreateFindByPageDataSpecification(int PageSize, int Page)
        {
            return new Specification(_paramCreator.CreateForFindByPageData(PageSize, Page));
        }
    }
}
