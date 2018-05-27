using System.Collections.Generic;   
using DM.PR.Common.Entities;
using DM.PR.Data.Repositories;

namespace DM.PR.Business.Providers.Implement
{
    class MaritalStatusProvider : IMaritalStatusProvider
    {
        private IMaritalStatusRepository _maritalStatusRepository;

        public MaritalStatusProvider(IMaritalStatusRepository maritalStatusRepository)
        {
            _maritalStatusRepository = maritalStatusRepository;
        }

        public IReadOnlyCollection<MaritalStatus> GetAll()
        {
            return _maritalStatusRepository.GetAll();
        }
    }
}
