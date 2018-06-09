using DM.PR.Data.Specifications;

namespace DM.PR.Data.SpecificationCreators
{
    public interface IEmployeeSpecificationCreator
    {
        ISpecification CreateFindByPageDataSpecification(int pageSize, int pageint);

        ISpecification CreateFindPageByDepartmentIdSpecification(int departmentId, int pageSize, int page);

        ISpecification CreateFindPageBySearchParamsSpecification(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page);
    }
}
