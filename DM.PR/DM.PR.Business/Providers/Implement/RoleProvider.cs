using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using DM.PR.Data.Repositories;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Providers.Implement
{
    internal class RoleProvider : IRoleProvider
    {
        private readonly IRepository<Role> _rep;

        public RoleProvider(IRepository<Role> rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _rep = rep;
        }
        public Role GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return _rep.GetById(id);
        }

        public IReadOnlyCollection<Role> GetAll()
        {
            return _rep.GetAll();
        }
    }
}
