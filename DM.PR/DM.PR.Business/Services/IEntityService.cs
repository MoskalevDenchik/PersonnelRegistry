namespace DM.PR.Business.Services
{
    public interface IEntityService<T>
    {
        void Create(T entity);
        void Edit(T entity);
        void Delete(int id);
    }
}
