using DM.PR.Data.Core.ParameterCreaters;
using DM.PR.Data.Specifications;
using DM.PR.Data.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Data.SpecificationCreators.Implement
{
    internal class EmployeeSpecificationCreator : IEmployeeSpecificationCreator
    {
        private IEmployeeParameterCreater _paramCreator;

        public EmployeeSpecificationCreator(IEmployeeParameterCreater paramCreator)
        {
            Inspector.ThrowExceptionIfNull(paramCreator);
            _paramCreator = paramCreator;
        }

        public ISpecification CreateFindByPageDataSpecification(int pageSize, int pageint)
        {
            return new Specification(_paramCreator.CreateForFindByPageData(pageSize, pageint));
        }

        public ISpecification CreateFindPageByDepartmentIdSpecification(int departmentId, int pageSize, int page)
        {
            return new Specification(_paramCreator.CreateForFindPageByDepartmentId(departmentId, pageSize, page));
        }

        public ISpecification CreateFindPageBySearchParamsSpecification(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page)
        {
            return new Specification(_paramCreator.CreateForFindPageBySearchParams(lastName, firstName, middledName, fromYear, toYear, WorkStatusId, pageSize, page));
        }
    }
}
