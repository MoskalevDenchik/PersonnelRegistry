using DM.PR.Common.Entities.Account;
using DM.PR.Data.Repositories;
using DM.PR.Business.Helpers;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System;

namespace DM.PR.Business.Services.Implement
{
    public class EntityService<T> : IEntityService<T>
    {
        #region Private

        private readonly IRepository<T> _rep;
        private readonly EntityValidator<T> _validator;

        #endregion

        #region Ctors

        public EntityService(IRepository<T> rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _validator = new EntityValidator<T>();
            _rep = rep;
        }

        #endregion

        public virtual Result Save(T entity)
        {
            var result = _validator.Validate(entity);

            if (result.Status == Status.Success)
            {

            }

            return result;
        }

        public virtual void Remove(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Invalid Id");
            }

            _rep.Remove(id);
        }
    }
}
