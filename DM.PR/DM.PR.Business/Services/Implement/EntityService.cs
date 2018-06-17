using DM.PR.Data.Repositories;
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

        public virtual void Save(T entity)
        {
            _rep.Save(entity);
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
