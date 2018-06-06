using DM.PR.Data.Entity;
using DM.PR.Data.Specifications;

namespace DM.PR.Data.Core.InputParameters.Creaters
{
    internal interface IParameterCreater<T>
    {
        IInputParameter CreateForGetById(int id);
        IInputParameter CreateForGetAll();
        IInputParameter CreateForFindBy(ISpecification specification);
        IInputParameter CreateForAdd(T item);
        IInputParameter CreateForRemove(int id);
        IInputParameter CreateForUpdate(T item);
    }
}
