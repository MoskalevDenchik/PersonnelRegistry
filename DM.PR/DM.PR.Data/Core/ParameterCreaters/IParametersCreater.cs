using DM.PR.Data.Specifications;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.InputParameters.Creaters
{
    internal abstract class ParameterCreater<T>
    {
        public IInputParameter CreateFindBy(ISpecification specification)
        {
            return specification.GetSpecific();
        }

        public abstract IInputParameter CreateGetById(int id);
        public abstract IInputParameter CreateGetAll();
        public abstract IInputParameter CreateAdd(T item);
        public abstract IInputParameter CreateRemove(int id);
        public abstract IInputParameter CreateUpdate(T item);
    }
}

