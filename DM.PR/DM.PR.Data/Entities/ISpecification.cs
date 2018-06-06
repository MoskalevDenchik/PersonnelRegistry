using DM.PR.Data.Entity;

namespace DM.PR.Data.Specifications
{
    public interface ISpecification
    {
        IInputParameter GetSpecific();
    }
}
