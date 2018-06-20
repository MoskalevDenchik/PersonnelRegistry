using DM.PR.Common.Entities;

namespace DM.PR.Business.Services
{
    public interface IEntityService<T>
    {
        Result Save(T item);
        void Remove(int id);
    }
}
