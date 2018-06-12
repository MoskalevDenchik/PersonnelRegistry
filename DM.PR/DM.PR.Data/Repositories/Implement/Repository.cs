using DM.PR.Data.Core.InputParameters.Creaters;
using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Data.Core.Data;
using DM.PR.Common.Helpers;

namespace DM.PR.Data.Repositories.Implement
{
    internal class Repository<T> : IRepository<T>
    {
        private readonly IDataContext<T> _dataContext;
        private readonly IParameterCreater<T> _parametersCreater;

        public Repository(IParameterCreater<T> creater, IDataContext<T> dataContext)
        {
            Inspector.ThrowExceptionIfNull(creater, dataContext);
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
        public IReadOnlyCollection<T> FindBy(ISpecification specification, out int outputParameter)
        {
            return _dataContext.GetEntities(_parametersCreater.CreateForFindBy(specification), out outputParameter);
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
