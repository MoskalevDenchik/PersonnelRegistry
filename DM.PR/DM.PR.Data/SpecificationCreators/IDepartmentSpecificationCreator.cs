namespace DM.PR.Data.Specifications
{
    public interface IDepartmentSpecificationCreator
    {
        ISpecification CreateFindByPageDataSpecification(int pageSize, int pageCount);
    }
}
