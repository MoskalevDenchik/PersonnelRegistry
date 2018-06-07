using System.Collections.Generic;
using DM.PR.Data.Specifications;

namespace DM.PR.Data.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IReadOnlyCollection<T> GetAll();
        IReadOnlyCollection<T> FindBy(ISpecification specification);
        IReadOnlyCollection<T> FindBy(ISpecification specification, out int outputParameter);
        void Add(T item);
        void Update(T item);
        void Remove(int id);
    }
}
