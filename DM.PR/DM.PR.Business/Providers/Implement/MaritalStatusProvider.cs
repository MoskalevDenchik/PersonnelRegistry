using System.Collections.Generic;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.Data.Repositories;

namespace DM.PR.Business.Providers.Implement
{
    internal class MaritalStatusProvider : IMaritalStatusProvider
    {
        private readonly IMaritalStatusRepository _maritalStatusRepository;

        public MaritalStatusProvider(IMaritalStatusRepository maritalStatusRepository)
        {
            Helper.ThrowExceptionIfNull(maritalStatusRepository);
            _maritalStatusRepository = maritalStatusRepository;
        }

        public IReadOnlyCollection<MaritalStatus> GetAll()
        {
            return _maritalStatusRepository.GetAll();
        }
    }
}
