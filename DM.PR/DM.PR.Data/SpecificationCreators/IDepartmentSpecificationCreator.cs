namespace DM.PR.Data.Specifications
{
    public interface IDepartmentSpecificationCreator
    {
        ISpecification CreateSpecification(int pageSize, int pageCount);
    }
}
