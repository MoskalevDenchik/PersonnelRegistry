using DM.PR.Common.Entities;
using DM.PR.Data.Specifications;
using System.Collections.Generic;

namespace DM.PR.Data.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IReadOnlyCollection<T> GetAll();
        PagedData<T> FindPageBy(ISpecification specification);
        IReadOnlyCollection<T> FindBy(ISpecification specification);
        void Add(T item);
        void Update(T item);
        void Remove(int id);
    }
}
