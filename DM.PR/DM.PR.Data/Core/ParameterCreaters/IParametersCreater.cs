using DM.PR.Data.Specifications;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.InputParameters.Creaters
{
    internal interface IParameterCreater<T>
    {
        IInputParameter CreateGetById(int id);
        IInputParameter CreateGetAll();
        IInputParameter CreateSave(T item);
        IInputParameter CreateRemove(int id);
    }
}

