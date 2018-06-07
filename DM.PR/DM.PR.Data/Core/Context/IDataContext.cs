using System.Collections.Generic;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.Data
{
    internal interface IDataContext<T>
    {
        IReadOnlyCollection<T> GetEntities(IInputParameter parameter);
        IReadOnlyCollection<T> GetEntities(IInputParameter parameter, out int outputParameter);
        T GetEntity(IInputParameter parameter);
        void Save(IInputParameter parameter);
    }
}
