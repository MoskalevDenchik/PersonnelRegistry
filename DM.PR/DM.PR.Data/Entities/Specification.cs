using DM.PR.Data.Entity;
using DM.PR.Data.Specifications;

namespace DM.PR.Data.Entities
{
    public class Specification : ISpecification
    {
        private IInputParameter _inputParameter;

        public Specification(IInputParameter inputParameter)
        {
            _inputParameter = inputParameter;
        }

        public IInputParameter GetSpecific()
        {
            return _inputParameter;
        }
    }
}
