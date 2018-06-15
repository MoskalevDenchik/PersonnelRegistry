using DM.PR.Data.Specifications;

namespace DM.PR.Data.SpecificationCreators
{
    public interface IUserSpecificationCreator
    {
        ISpecification CreateSpecification(string login);
    }
}
