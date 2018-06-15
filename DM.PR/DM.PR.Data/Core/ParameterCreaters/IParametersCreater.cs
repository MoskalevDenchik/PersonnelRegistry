using DM.PR.Data.Specifications;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.InputParameters.Creaters
{
    internal abstract class IParameterCreater<T>
    {
        public IInputParameter CreateForFindBy(ISpecification specification)
        {
            return specification.GetSpecific();
        }

        public abstract IInputParameter CreateForGetById(int id);
        public abstract IInputParameter CreateForGetAll();
        public abstract IInputParameter CreateForAdd(T item);
        public abstract IInputParameter CreateForRemove(int id);
        public abstract IInputParameter CreateForUpdate(T item);
    }
}

