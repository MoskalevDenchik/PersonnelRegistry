using System.Collections.Generic;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Providers.Implement
{
    internal class MaritalStatusProvider : IMaritalStatusProvider
    {
        private readonly IRepository<MaritalStatus> _rep;

        public MaritalStatusProvider(IRepository<MaritalStatus> maritalStatusRepository)
        {
            Inspector.ThrowExceptionIfNull(maritalStatusRepository);
            _rep = maritalStatusRepository;
        }

        public MaritalStatus GetById(int id)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(id);
            return _rep.GetById(id);
        }

        public IReadOnlyCollection<MaritalStatus> GetAll()
        {
            return _rep.GetAll();
        }
    }
}
