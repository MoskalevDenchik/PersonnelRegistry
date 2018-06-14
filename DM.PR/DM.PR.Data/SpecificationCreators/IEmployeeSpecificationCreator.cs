using DM.PR.Data.Specifications;

namespace DM.PR.Data.SpecificationCreators
{
    public interface IEmployeeSpecificationCreator
    {
        ISpecification CreateSpecification(int pageSize, int pageint);

        ISpecification CreateSpecification(int departmentId, int pageSize, int page);

        ISpecification CreateSpecification(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page);
    }
}
