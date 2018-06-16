using System.Collections.Generic;
using DM.PR.Data.Repositories;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Providers
{
    public interface IProvider<T>
    {
        IReadOnlyCollection<T> GetAll();
        T GetById(int id);
    }

}
