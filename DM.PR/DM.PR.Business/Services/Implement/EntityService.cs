using System.ComponentModel.DataAnnotations;
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System;

namespace DM.PR.Business.Services.Implement
{
    public class EntityService<T> : IEntityService<T>
    {
        private readonly IRepository<T> _rep;

        public EntityService(IRepository<T> rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _rep = rep;
        }

        public virtual Result Save(T entity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity);
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                return new Result { Exception = results, Status = Status.InValid };
            }
            else
            {
                _rep.Save(entity);
                return new Result { Status = Status.Success };
            }
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
