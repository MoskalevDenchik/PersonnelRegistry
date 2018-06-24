using DM.PR.Common.Entities.Account;
using DM.PR.Data.Repositories;
using DM.PR.Business.Helpers;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DM.PR.Business.Services.Implement
{
    internal class EntityService<T> : IEntityService<T>
    {
        #region Private

        private readonly IRepository<T> _rep;

        #endregion

        #region Ctors

        public EntityService(IRepository<T> rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _rep = rep;
        }

        #endregion

        public virtual Result Save(T entity)
        {
            var result = new Result();

            if (IsValidByAttributes(result, entity))
            {
                if (IsValid(result, entity))
                {
                    _rep.Save(entity);
                }
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

        protected virtual bool IsValid(Result result, T enity)
        {
            return true;
        }

        protected bool IsValidByAttributes(Result result, T entity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity);
            if (Validator.TryValidateObject(entity, context, results, true))
            {
                result.Status = Status.Success;
                result.Exceptions = null;
                return true;
            }
            else
            {
                result.Status = Status.Failure;
                result.Exceptions = results;
                return false;
            }
        }

    }
}
