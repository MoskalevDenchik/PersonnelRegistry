using DM.PR.Data.Core.ParameterCreaters;
using DM.PR.Data.Specifications;
using DM.PR.Common.Helpers;
using DM.PR.Data.Entities;

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

        public ISpecification CreateSpecification(int pageSize, int pageint)
        {
            return new Specification(_paramCreator.CreateFind(pageSize, pageint));
        }

        public ISpecification CreateSpecification(int departmentId, int pageSize, int page)
        {
            return new Specification(_paramCreator.CreateByDepartmentId(departmentId, pageSize, page));
        }

        public ISpecification CreateSpecification(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page)
        {
            return new Specification(_paramCreator.CreateBySearchParams(lastName, firstName, middledName, fromYear, toYear, WorkStatusId, pageSize, page));
        }
    }
}
