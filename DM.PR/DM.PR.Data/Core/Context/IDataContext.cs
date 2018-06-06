using System.Collections.Generic;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.Data
{
    internal interface IDataContext<T>
    {
        IReadOnlyCollection<T> GetEntities(IInputParameter parameter);
        PagedData<T> GetPageEntities(IInputParameter parameter);
        T GetEntity(IInputParameter parameter);
        void Save(IInputParameter parameter);
    }
}
