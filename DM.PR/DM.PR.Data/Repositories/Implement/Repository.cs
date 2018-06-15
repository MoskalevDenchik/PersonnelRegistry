using DM.PR.Data.Core.InputParameters.Creaters;
using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Data.Core.Data;
using DM.PR.Common.Helpers;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Repositories.Implement
{
    internal class Repository<T> : IRepository<T>
    {
        private readonly IDataContext<T> _dataContext;
        private readonly ParameterCreater<T> _parameterCreater;

        public Repository(ParameterCreater<T> creater, IDataContext<T> dataContext)
        {
            Inspector.ThrowExceptionIfNull(creater, dataContext);
            _dataContext = dataContext;
            _parameterCreater = creater;
        }

        public T GetById(int id)
        {
            IInputParameter getByIdParameter = _parameterCreater.CreateGetById(id);
            return _dataContext.GetEntity(getByIdParameter);
        }

        public IReadOnlyCollection<T> GetAll()
        {
            IInputParameter getAllParameter = _parameterCreater.CreateGetAll();
            return _dataContext.GetEntities(getAllParameter);
        }

        public IReadOnlyCollection<T> FindBy(ISpecification specification)
        {
            IInputParameter findByParameter = _parameterCreater.CreateFindBy(specification);
            return _dataContext.GetEntities(findByParameter);
        }

        public IReadOnlyCollection<T> FindBy(ISpecification specification, out int outputParameter)
        {
            IInputParameter findByParameter = _parameterCreater.CreateFindBy(specification);
            return _dataContext.GetEntities(findByParameter, out outputParameter);
        }

        public void Add(T item)
        {
            IInputParameter addByParameter = _parameterCreater.CreateAdd(item);
            _dataContext.Save(addByParameter);
        }

        public void Remove(int id)
        {
            IInputParameter removeByParameter = _parameterCreater.CreateRemove(id);
            _dataContext.Save(removeByParameter);
        }

        public void Update(T item)
        {
            IInputParameter updateByParameter = _parameterCreater.CreateUpdate(item);
            _dataContext.Save(updateByParameter);
        }
    }
}
