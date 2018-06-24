namespace DM.PR.Data.Specifications
{
    public interface IDepartmentSpecificationCreator
    {
        ISpecification CreateSpecification(int parentId);
        ISpecification CreateSpecification(int pageSize, int pageCount);
        ISpecification CreateSpecification(string name);
    }
}
