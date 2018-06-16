using System.Collections.Generic;
using DM.PR.Data.Repositories;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Providers.Implement
{

    public class Provider<T> : IProvider<T> where T : class
    {
        private readonly IRepository<T> _rep;

        public Provider(IRepository<T> rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _rep = rep;
        }

        public virtual T GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return _rep.GetById(id);
        }
        public virtual IReadOnlyCollection<T> GetAll()
        {
            return _rep.GetAll();
        }
    }
}
