using System.Collections.Generic;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System;

namespace DM.PR.Business.Providers.Implement
{
    internal class MaritalStatusProvider : IMaritalStatusProvider
    {
        private readonly IRepository<MaritalStatus> _rep;

        public MaritalStatusProvider(IRepository<MaritalStatus> maritalStatusRepository)
        {
            Helper.ThrowExceptionIfNull(maritalStatusRepository);
            _rep = maritalStatusRepository;
        }

        public MaritalStatus GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Неверный ID");
            }

            return _rep.GetById(id);
        }

        public IReadOnlyCollection<MaritalStatus> GetAll()
        {
            return _rep.GetAll();
        }
    }
}
