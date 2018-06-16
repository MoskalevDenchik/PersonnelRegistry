using System.Collections.Generic;   

namespace DM.PR.Business.Providers
{
    public interface IProvider<T>
    {
        T GetById(int id);
        IReadOnlyCollection<T> GetAll();
    }

}
