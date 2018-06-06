using System.Collections.Generic;
using DM.PR.Common.Entities;
using DM.PR.Data.Core.Data;
using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Data.Specifications;

namespace DM.PR.Data.Repositories.Implement
{
    internal class Repository<T> : IRepository<T>
    {
        private readonly IDataContext<T> _dataContext;
        private readonly IParameterCreater<T> _parametersCreater;

        public Repository(IParameterCreater<T> creater, IDataContext<T> dataContext)
        {
            _dataContext = dataContext;
            _parametersCreater = creater;
        }

        public T GetById(int id)
        {
            return _dataContext.GetEntity(_parametersCreater.CreateForGetById(id));
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return _dataContext.GetEntities(_parametersCreater.CreateForGetAll());
        }

        public IReadOnlyCollection<T> FindBy(ISpecification specification)
        {
            return _dataContext.GetEntities(_parametersCreater.CreateForFindBy(specification));
        }

        public PagedData<T> FindPageBy(ISpecification specification)
        {
            return _dataContext.GetPageEntities(_parametersCreater.CreateForFindBy(specification));
        }

        public void Add(T item)
        {
            _dataContext.Save(_parametersCreater.CreateForAdd(item));
        }

        public void Remove(int id)
        {
            _dataContext.Save(_parametersCreater.CreateForRemove(id));
        }

        public void Update(T item)
        {
            _dataContext.Save(_parametersCreater.CreateForUpdate(item));
        }
    }
}
