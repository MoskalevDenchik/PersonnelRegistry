namespace DM.PR.Business.Services
{
    public interface IEntityService<T>
    {
        void Save(T item);
        void Remove(int id);
    }
}
